using PhoneBook.Services.TokenServices;

namespace PhoneBook.Services.UserServices
{
    public class LoginUser
    {
        private readonly GenerateTokenService _generateTokenService;
        private readonly GetUserByUsernameAndPasswordService _getUserService;
        public LoginUser(GenerateTokenService generateTokenService, GetUserByUsernameAndPasswordService getUserService)
        {
            _generateTokenService = generateTokenService;
            _getUserService = getUserService;
        }
        public async Task<string> LoginUserAsync(string username, string password)
        {
            var user = await _getUserService.GetUserByUsernameAndPasswordAsync(username, password);
            if (user == null)
            {
                throw new InvalidOperationException("Kullanıcı adı veya sifre hatalı");
            }
            return _generateTokenService.GenerateToken(user);
        }
    }
}