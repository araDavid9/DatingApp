using System.Collections;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private ITokenService m_Token;

    public AccountController(DataContext i_DbContext, ITokenService i_TokenServ) : base(i_DbContext)
    {
        m_Token = i_TokenServ;
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto i_regDto)
    {
        ActionResult returnState = BadRequest("User name already exists!");
        
        if (! await isUserNameExists(i_regDto.UserName))
        {
            using (HMACSHA512 hash = new HMACSHA512())
            {
                AppUser newUser= new AppUser
                {
                    UserName = i_regDto.UserName,
                    PasswordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(i_regDto.Password)),
                    PasswordSalt = hash.Key
                };
                m_DbContext.Add(newUser);
                await m_DbContext.SaveChangesAsync();
                UserDto newUserDto = new UserDto
                {
                    UserName = newUser.UserName,
                    Token = m_Token.CreateToken(newUser)
                };
                returnState = Ok(newUserDto);
            }
        }
        
        return returnState;
    }

    
    [HttpPost("Login")] // ..../Account/Login
    public async Task<ActionResult<UserDto>> Login(LoginDto i_LogDto)
    {
        ActionResult returnState = Unauthorized("Wrong UserName");
        var currentUser = await m_DbContext.Users.FirstOrDefaultAsync(line => 
            i_LogDto.UserName == line.UserName);
        
        if (currentUser != null)
        {
            using (HMACSHA512 hash = new HMACSHA512(currentUser.PasswordSalt))
            {
                UserDto currUserDto = new UserDto
                {
                    UserName = currentUser.UserName,
                    Token = m_Token.CreateToken(currentUser)
                };
                
                returnState = Ok(currUserDto);
                var computeHash = hash.ComputeHash(Encoding.UTF8.GetBytes(i_LogDto.Password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != currentUser.PasswordHash[i])
                    {
                        returnState = Unauthorized("Wrong Password");
                        break;
                    }
                }
            }
        }

        return returnState;

    }

    private Task<bool> isUserNameExists(string i_UserName)
    {
        return m_DbContext.Users.AnyAsync(line => i_UserName.ToLower() == line.UserName);
    }

}