using Microsoft.Data.SqlClient;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Services.ContactServices
{
    public class UpdateContact
    {
        private readonly string _connectionString;
        private readonly GetContactByPhoneNumber _getContactByPhoneNumber;

        public UpdateContact(IConfiguration configuration, GetContactByPhoneNumber getContactByPhoneNumber)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _getContactByPhoneNumber = getContactByPhoneNumber;
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            if (contact == null || string.IsNullOrEmpty(contact.PhoneNumber))
            {
                throw new ArgumentNullException(nameof(contact), "İletisim kisisi bos olamaz");;
            }
            var exitingContact = await _getContactByPhoneNumber.GetContactByPhoneNumberAsync(contact.PhoneNumber, contact.UserId);
            if (exitingContact == null)
            {
                throw new InvalidOperationException ("Bu numara bulunamadi. Lütfen geçerli bir numara giriniz.");
            }
            string commandText = "UPDATE Contacts SET FirstName = @FirstName, LastName = @LastName WHERE PhoneNumber = @PhoneNumber AND UserId = @UserId";
            var parameters = new[] 
            {
                new SqlParameter("@FirstName", contact.FirstName),
                new SqlParameter("@LastName", contact.LastName),
                new SqlParameter("@PhoneNumber", contact.PhoneNumber),
                new SqlParameter("@UserId", contact.UserId)
            };
            await DatabaseHelper.ExecuteNonQueryAsync(_connectionString, commandText, parameters);
        }
    }
}