using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Web.Controllers;

[ApiController]
[Route("api/v0.1/[controller]")]
public class ExampleController : ControllerBase
{
    [HttpGet("{id}")]
    public Example Get(int id)
    {
        return new Example($"Message {id}");
    }
    
    [HttpPost]
    public Example Post(string message)
    {
        return new Example(message);
    }
}