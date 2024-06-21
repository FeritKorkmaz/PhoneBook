using Microsoft.Data.SqlClient;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Services.ContactServices
{
    public class DeleteContact
    {
        private readonly string _connectionString;
        private readonly GetContactByPhoneNumber _getContactByPhoneNumber;

        public DeleteContact(string connectionString, GetContactByPhoneNumber getContactByPhoneNumber)
        {
            _connectionString = connectionString;
            _getContactByPhoneNumber = getContactByPhoneNumber;
        }

        public async Task DeleteContactAsync(string phoneNumber)
        {
            var exitingContact = await _getContactByPhoneNumber.GetContactByPhoneNumberAsync(phoneNumber);
            if (exitingContact == null)
            {
                throw new InvalidOperationException("Bu kayit bulunamadi");
            }   
            string commandText = "DELETE FROM Contacts WHERE PhoneNumber = @PhoneNumber";
            var parameters = new[] 
            {
                new SqlParameter("@PhoneNumber", phoneNumber)
            }; 
            await DatabaseHelper.ExecuteNonQueryAsync(_connectionString, commandText, parameters);
        }
    }
}