using API.Models;

namespace API.Interfaces;

public interface ITokenService
{
    public string CreateToken(AppUser i_User);
    
}