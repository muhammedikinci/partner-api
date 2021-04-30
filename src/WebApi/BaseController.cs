using Microsoft.AspNetCore.Mvc;

namespace WebApi
{
    public class BaseController : ControllerBase
    {
        public IActionResult SetResponse<T>(T data)
        {
            if (data == null)
                return Ok(false);

            return Ok(data);
        }
    }
}