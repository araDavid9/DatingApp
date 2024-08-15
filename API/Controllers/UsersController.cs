using System.Collections;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class UsersController : BaseApiController
{
    public UsersController(DataContext i_Context) : base(i_Context){}
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await m_DbContext.Users.ToListAsync();
        return Ok(users) ; // Response 200 
    }

    [Authorize]
    [HttpGet("{i_id}")]// .../api/Users/i_id
    public async Task<ActionResult<AppUser>> GetUserById(int i_id)
    {
        var user = await m_DbContext.Users.FindAsync(i_id);
        return user != null ? Ok(user) : NotFound();

    }
}