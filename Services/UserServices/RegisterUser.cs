using Microsoft.Data.SqlClient;
using PhoneBook.Data;

namespace PhoneBook.Services.UserServices
{
    public class RegisterUser
    {
        private readonly string _connectionString;

        public RegisterUser(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task RegisterUserAsync(string username, string password)
        {
            string commandText = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            var parameters = new[] 
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };
            try
            {
                await DatabaseHelper.ExecuteNonQueryAsync(_connectionString, commandText, parameters);
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException ("Kullanıcı adı zaten kullanılıyor. Lutfen farklı bir kullanıcı adı giriniz.", ex);
            }
        }
    }
}