using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingApp.Api.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public MyErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; 
            var code = 500; 

            if (exception is MyNotFoundException) code = 404; 
            else if (exception is MyUnauthorizedException) code = 401; 
            else if (exception is MyException) code = 400; 

            Response.StatusCode = code; 

            return new MyErrorResponse(exception); 
        }
    }
}