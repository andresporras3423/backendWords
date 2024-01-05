using backendWords.classes;
using backendWords.MappingClasses;
using backendWords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace backendWords.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly testContext _context;

        public UsersController(testContext context)
        {
            _context = context;
        }

        [HttpGet]
        [CustomActionFilter]
        public async Task<ActionResult> Get()
        {
            return Ok(new { id = HttpContext.Items["id"] });
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserMap user0_)
        {
            try
            {
                if (user0_.password != user0_.password_confirmation)
                {
                    return BadRequest(new { error = "password and password confirmation are different" });
                }
                var user_ = user0_.generateUser();
                user_.PasswordDigest = Session.HashPassword(user_.PasswordDigest);
                _context.Users.Add(user_);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            return Ok();
        }
 
    }
}
