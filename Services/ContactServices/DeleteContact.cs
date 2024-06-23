using Microsoft.Data.SqlClient;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Services.ContactServices
{
    public class DeleteContact
    {
        private readonly string _connectionString;
        private readonly GetContactByPhoneNumber _getContactByPhoneNumber;

        public DeleteContact(IConfiguration configuration, GetContactByPhoneNumber getContactByPhoneNumber)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _getContactByPhoneNumber = getContactByPhoneNumber;
        }

        public async Task DeleteContactAsync(string phoneNumber, int userId)
        {
            var exitingContact = await _getContactByPhoneNumber.GetContactByPhoneNumberAsync(phoneNumber, userId);
            if (exitingContact == null)
            {
                throw new InvalidOperationException("Bu kayit bulunamadi");
            }   
            string commandText = "DELETE FROM Contacts WHERE PhoneNumber = @PhoneNumber AND UserId = @UserId";
            var parameters = new[] 
            {
                new SqlParameter("@PhoneNumber", phoneNumber),
                new SqlParameter("@UserId", userId)
            }; 
            await DatabaseHelper.ExecuteNonQueryAsync(_connectionString, commandText, parameters);
        }
    }
}