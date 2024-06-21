using Microsoft.Data.SqlClient;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Services.ContactServices
{
    public class GetContactsByUserId
    {
        private readonly string _connectionString;
        public GetContactsByUserId(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Contact>> GetContactsByUserIdAsync(int userId)
        {
            // SQL sorgusu: Kullanıcıyı kullanıcı adı ve şifresine göre seçilir
            string commandText = "SELECT Id, UserId, FirstName, LastName, PhoneNumber FROM Contacts WHERE UserId = @UserId";
            // SQL sorgusuna kullanıcı adı ve şifre parametreleri eklenir
            var parameters =new [] 
            {
                new SqlParameter("@UserId",userId),
            };  

            // DatabaseHelper kullanılarak SQL sorgusu çalıştırılır ve sonuçlar okunur    
            var contacts = await DatabaseHelper.ExecuteReaderAsync(_connectionString, commandText, reader => new Contact
            {
                // User nesnesi oluşturulurken veritabanından okunan değerler User nesnesine atanır. 
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                FirstName = reader.GetString(2),
                LastName = reader.GetString(3),
                PhoneNumber = reader.GetString(4)
            }, parameters);
            
            return contacts;
        }
    }   
}