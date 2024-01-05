using backendWords.classes;
using backendWords.MappingClasses;
using backendWords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendWords.Controllers
{
    [Route("[controller]")]
    [CustomActionFilter]
    [ApiController]
    public class TestsController : ControllerBase
    {

        private readonly testContext _context;

        public TestsController(testContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TestMap test0_)
        {
            try
            {
                var test_ = test0_.generateTest();
                var userId = HttpContext.Items["id"];
                test_.UserId = Convert.ToInt32(userId);
                _context.Tests.Add(test_);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var userId = HttpContext.Items["id"];
                var commandText = "SELECT * FROM tests WHERE user_id="+userId+" order by created_at desc";
                var result = await _context.Set<Test>().FromSqlRaw(commandText).ToListAsync();
                var nresult = MappingContext.testToTestMaps(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            
        }
    }
}
