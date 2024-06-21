using Microsoft.Data.SqlClient;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Services.ContactServices
{
    public class CreateContact
    {
        private readonly string _connectionString;
        private readonly GetContactByPhoneNumber _getContactByPhoneNumber;

        public CreateContact(string connectionString, GetContactByPhoneNumber getContactByPhoneNumber)
        {
            _connectionString = connectionString;
            _getContactByPhoneNumber = getContactByPhoneNumber;
        }

        public async Task CreateContactAsync(Contact contact)
        {
            if (contact == null || string.IsNullOrEmpty(contact.PhoneNumber))
            {
                throw new ArgumentNullException(nameof(contact), "İletisim kisisi bos olamaz");;
            }
            var exitingContact = await _getContactByPhoneNumber.GetContactByPhoneNumberAsync(contact.PhoneNumber);
            if (exitingContact != null)
            {
                throw new InvalidOperationException ("Bu numara zaten ekli. Lutfen farkli bir numara giriniz.");
            }
            string commandText = "INSERT INTO Contacts (UserId, FirstName, LastName, PhoneNumber) VALUES (@UserId, @FirstName, @LastName, @PhoneNumber)";
            var parameters = new[] 
            {
                new SqlParameter("@UserId", contact.UserId),
                new SqlParameter("@FirstName", contact.FirstName),
                new SqlParameter("@LastName", contact.LastName),
                new SqlParameter("@PhoneNumber", contact.PhoneNumber)
            };
            await DatabaseHelper.ExecuteNonQueryAsync(_connectionString, commandText, parameters);
        }
    }
}