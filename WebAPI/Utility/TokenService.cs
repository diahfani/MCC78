using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.ViewModels.Others;

namespace WebAPI.Utility;


public interface ITokenService
{
    //payload di jwt = claim
    string GenerateToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    ClaimVM ExtractClaimsFromJwt(string token);


}
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ClaimVM ExtractClaimsFromJwt(string token)
    {
        if (token.IsNullOrEmpty()) return new ClaimVM();

        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, //dibuat false karna belum disetting issuer
                ValidateAudience = false, // sama kaya validate issuer tp yg belum di setting audience
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
            };

            //parse dan validate jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity)
            {
                var claims = new ClaimVM
                {
                    NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                    Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                    Email = identity.FindFirst(ClaimTypes.Email)!.Value
                };

                var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                claims.Role = roles;
                return claims;
            }
        } catch
        {
            return new ClaimVM();
        }
        return new ClaimVM();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        //diambil dari appsetting.json. string acak yg di key tadi di convert ke 26 byte
        var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        // secret key dienkripsi dengan security algorithm
        var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
        // tokenoptions untuk membentuk pola dari payload
        var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: signinCredentials);
        // mengembalikan tokenoptions menjadi string
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return tokenString;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        throw new NotImplementedException();
    }
}
