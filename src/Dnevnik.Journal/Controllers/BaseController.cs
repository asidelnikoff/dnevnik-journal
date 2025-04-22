using Microsoft.AspNetCore.Mvc;

namespace Dnevnik.Journal.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}")]
public class BaseController : ControllerBase
{
    
}