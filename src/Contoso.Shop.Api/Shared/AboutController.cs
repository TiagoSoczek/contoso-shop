using Microsoft.AspNetCore.Mvc;

namespace Contoso.Shop.Api.Shared
{
    [Route(RouteConstants.Controller)]
    [Route("")]
    public class AboutController : Controller
    {
        public IActionResult Get()
        {
            return Ok("Contoso.Api");
        }
    }
}