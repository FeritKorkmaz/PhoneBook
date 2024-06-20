using Microsoft.Data.SqlClient;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Services.UserServices
{
    public class GetUserByUsernameAndPasswordService
    {
        private readonly string _connectionString;
        public GetUserByUsernameAndPasswordService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            // SQL sorgusu: Kullanıcıyı kullanıcı adı ve şifresine göre seçilir
            string commandText = "SELECT Id, Username, Password FROM Users WHERE Username = @Username AND Password = @Password";
            // SQL sorgusuna kullanıcı adı ve şifre parametreleri eklenir
            var parameters =new [] 
            {
                new SqlParameter("@Username",username),
                new SqlParameter("@Password",password)
            };  

            // DatabaseHelper kullanılarak SQL sorgusu çalıştırılır ve sonuçlar okunur    
            var user = await DatabaseHelper.ExecuteReaderAsync(_connectionString, commandText, reader => new User
            {
                // User nesnesi oluşturulurken veritabanından okunan değerler User nesnesine atanır. 
                Id = reader.GetInt32(0),
                UserName = reader.GetString(1),
                Password = reader.GetString(2)
            }, parameters);
            
            return user.FirstOrDefault() ?? throw new InvalidOperationException("User not found");
        }
        
    }
    
}