using Microsoft.Data.SqlClient;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Services.ContactServices
{
    public class GetContactByPhoneNumber
    {
        private readonly string _connectionString;
        public GetContactByPhoneNumber(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Contact?> GetContactByPhoneNumberAsync(string phoneNumber)
        {
            // SQL sorgusu: Kullanıcıyı kullanıcı adı ve şifresine göre seçilir
            string commandText = "SELECT Id, UserId, FirstName, LastName, PhoneNumber FROM Contacts WHERE PhoneNumber = @PhoneNumber";
            // SQL sorgusuna kullanıcı adı ve şifre parametreleri eklenir
            var parameters =new [] 
            {
                new SqlParameter("@PhoneNumber",phoneNumber),
            };  

            // DatabaseHelper kullanılarak SQL sorgusu çalıştırılır ve sonuçlar okunur    
            var contact = await DatabaseHelper.ExecuteReaderAsync(_connectionString, commandText, reader => new Contact
            {
                // User nesnesi oluşturulurken veritabanından okunan değerler User nesnesine atanır. 
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                FirstName = reader.GetString(2),
                LastName = reader.GetString(3),
                PhoneNumber = reader.GetString(4)
            }, parameters);
            
            return contact.FirstOrDefault();
        }
    }   
}