using System.Collections;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // controller will be swaped with the first part of the class name
public class UsersController : ControllerBase
{
    private readonly DataContext m_DbContext;

    public UsersController(DataContext i_Context)
    {
        m_DbContext = i_Context;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await m_DbContext.Users.ToListAsync();
        return Ok(users) ; // Response 200 
    }

    [HttpGet]
    [Route("{i_id}")] // .../api/Users/i_id
    public async Task<ActionResult<AppUser>> GetUserById(int i_id)
    {
        var user = await m_DbContext.Users.FindAsync(i_id);
        return user != null ? Ok(user) : NotFound();

    }
}