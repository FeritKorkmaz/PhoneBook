using Microsoft.Data.SqlClient;
using PhoneBook.Data;

namespace PhoneBook.Services.UserServices
{
    public class RegisterUser
{
    private readonly string _connectionString;

    public RegisterUser(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection"); // configuration'dan bağlantı dizesi alınır
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
            Console.WriteLine(ex.Message);
            throw new InvalidOperationException("Kullanici adi zaten kullaniliyor. Lutfen farkli bir kullanici adi giriniz.", ex);
        }
    }
}

}