using System.Collections;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // controller will be swaped with the first part of the class name
public class BaseApiController : ControllerBase
{
    protected readonly DataContext m_DbContext;

    public BaseApiController(DataContext i_Context)
    {
        m_DbContext = i_Context;
    }
}