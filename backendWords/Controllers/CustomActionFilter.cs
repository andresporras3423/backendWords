using backendWords.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace backendWords.Controllers
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        private readonly testContext _context;

        public CustomActionFilter()
        {
            _context = new testContext();
        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            // Code here runs before the action method execution
            // Get the HTTP context
            try
            {
                var httpContext = context.HttpContext;

                // Read the headers
                context.HttpContext.Items["id"] = httpContext.Request.Headers["id"].FirstOrDefault();
                context.HttpContext.Items["token"] = httpContext.Request.Headers["token"].FirstOrDefault();
                if (context.HttpContext.Items["id"] == null | context.HttpContext.Items["token"]==null) throw new Exception("missing Id or token");
                var _user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(context.HttpContext.Items["id"]));
                if (_user == null | _user.RememberToken != context.HttpContext.Items["token"]) throw new Exception("incorrect Id or token");
                base.OnActionExecuting(context);
            }
            catch(Exception ex) {
                context.Result = new BadRequestObjectResult(new { error = ex.Message });
            }
            
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Code here runs after the action method execution
            base.OnActionExecuted(context);
        }
    }
}
