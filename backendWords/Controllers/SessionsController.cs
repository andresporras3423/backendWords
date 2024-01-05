using backendWords.classes;
using backendWords.MappingClasses;
using backendWords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendWords.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly testContext _context;

        public SessionsController(testContext context)
        {
            _context = context;
        }

        // Método para crear una sesión
        [HttpPost]
        public async Task<ActionResult> Post(UserMap user0_)
        {
            // Lógica para crear la sesión
            // ...
            var user_ = user0_.generateUser();
            var _user = await _context.Users.FirstOrDefaultAsync(u => u.Email == user_.Email);
            if (_user == null) return BadRequest(new { error = "email not found" });
            var passwordIsCorrect = Session.VerifyPassword(user_.PasswordDigest, _user.PasswordDigest);
            if (!passwordIsCorrect) return BadRequest(new { error = "wrong password" });
            var newToken = Session.GenerateRememberToken();
            _user.RememberToken = newToken;
            await _context.SaveChangesAsync();
            return Ok(new { remember_token = newToken, id = _user.Id }); // O algún otro resultado adecuado
        }

        [HttpDelete]
        [CustomActionFilter]
        public async Task<ActionResult> Delete()
        {
            // Lógica para crear la sesión
            var user_ = await _context.Users.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(HttpContext.Items["id"]));
            user_.RememberToken = null;
            await _context.SaveChangesAsync();
            return Ok(); // O algún otro resultado adecuado
        }
    }
}
