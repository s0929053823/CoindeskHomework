using CoindeskHomework.Controllers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CoindeskHomework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ApiLoggingFilter))]
    public  class BaseApiController : ControllerBase
    {
    }
}
