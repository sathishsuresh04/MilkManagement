using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkManagement.Web.Error;
using System;

namespace MilkManagement.Web.Controllers
{
	[ApiController]
  [ApiExplorerSettings(IgnoreApi = true)]
  [AllowAnonymous]
  public class ErrorController : ControllerBase
	{
    [Route("/error-local-development")]
    public IActionResult ErrorLocalDevelopment(
        [FromServices] IWebHostEnvironment webHostEnvironment)
    {
      if (webHostEnvironment.EnvironmentName != "Development")
      {
        throw new InvalidOperationException(
            "This shouldn't be invoked in non-development environments.");
      }
      var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
      var exception = context.Error;

      return Problem(
          detail: exception.StackTrace,
          type: exception.GetType().Name,
          title: exception.Message); ;
    }
    [Route("/error")]
		public IActionResult Error() => Problem();

	}
}
