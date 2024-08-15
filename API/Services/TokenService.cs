using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Interfaces;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService: ITokenService
{
    private IConfiguration m_Config;

    public TokenService(IConfiguration i_Config)
    {
        m_Config = i_Config;
    }
    public string CreateToken(AppUser i_User)
    {
        string tokenKey = m_Config["TokenKey"] ?? throw new Exception("Cannot Access to tokenKey"); // ?? = if(..) == null
        if (tokenKey.Length < 64)
        {
            throw new Exception("the tokenKey is too short!");
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)); // symertic key means that also decript will use the same key
        List<Claim> claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, i_User.UserName) // can add more claims
        };
        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // Creating the signature part
        SecurityTokenDescriptor desc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Today.AddDays(7), // the token will expire after a 1 week
            SigningCredentials = cred

        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(desc);

        return tokenHandler.WriteToken(token);



    }
    
}