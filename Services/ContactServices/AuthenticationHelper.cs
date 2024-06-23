using System.IdentityModel.Tokens.Jwt;
using Microsoft.JSInterop;

public class AuthenticationHelper
{
    private readonly IJSRuntime _jsRuntime;

    // Constructor: AuthenticationHelper sınıfı oluşturulurken IJSRuntime alır
    public AuthenticationHelper(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // GetUserIdFromTokenAsync metodu: JWT token'dan UserId'yi alır
    public async Task<int?> GetUserIdFromTokenAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var handler = new JwtSecurityTokenHandler(); // JWT token handler oluşturulur
        var jwtToken = handler.ReadJwtToken(token); // Token okunur
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId"); // Token'dan UserId claim'i alınır

        if (userIdClaim == null)
        {
            return null;
        }
        // UserId claim'i varsa, int'e dönüştürülüp döner
        return int.Parse(userIdClaim.Value);
    }
}
