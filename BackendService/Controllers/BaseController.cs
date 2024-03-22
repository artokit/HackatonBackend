using Common;
using Microsoft.AspNetCore.Mvc;

namespace EducationService.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected int UserId => AuthHeader.GetUserId();
    protected bool IsAdmin => AuthHeader.IsAdmin();
    protected string AuthHeader => HttpContext.Request.Headers["Authorization"].ToString();
}
