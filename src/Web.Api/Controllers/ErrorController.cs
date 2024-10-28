using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeamCounters.Web.Api.Controllers;

/// <summary>
/// Provides controller for default error handling
/// </summary>
[ApiController, AllowAnonymous]
public class ErrorController : ControllerBase
{
    /// <summary>
    /// Returns error response
    /// </summary>
    /// <returns></returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult ErrorHandler() => Problem();
}
