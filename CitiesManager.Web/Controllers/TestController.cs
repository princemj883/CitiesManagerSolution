using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Method()
    {
        return "Hello World!";
    }
        
}