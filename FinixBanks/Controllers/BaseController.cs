using FinixBanks.Core.General;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinixBanks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        private IMediator _mediator;
        private IAntiforgery _antiforgery;
        protected IMediator Mediator => _mediator ?? (_mediator =
            HttpContext.RequestServices.GetService<IMediator>());
        protected IAntiforgery Antiforgery => _antiforgery ?? (_antiforgery =
            HttpContext.RequestServices.GetService<IAntiforgery>());

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.StatusCode == (int)HttpStatusCode.Unauthorized)
                return Unauthorized();
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}
