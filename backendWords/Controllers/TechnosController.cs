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
    public class TechnosController : ControllerBase
    {

        private readonly testContext _context;

        public TechnosController(testContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TechnoMap techno0_)
        {
            try
            {
                var techno_ = techno0_.generateTechno();
                var userId = HttpContext.Items["id"];
                techno_.UserId = Convert.ToInt32(userId);
                _context.Technos.Add(techno_);
                await _context.SaveChangesAsync();
                techno0_ = techno_.generateTechnoMap();
                return Ok(techno0_);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(TechnoMap techno0_)
        {
            try
            {
                var techno_ = techno0_.generateTechno();
                var oldTechno = await _context.Technos.FirstOrDefaultAsync(t => t.Id == techno_.Id);
                oldTechno.TechnoName = techno_.TechnoName;
                oldTechno.TechnoStatus = techno_.TechnoStatus;
                await _context.SaveChangesAsync();
                techno0_ = oldTechno.generateTechnoMap();
                return Ok(techno0_);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var oldTechno = await _context.Technos.FirstOrDefaultAsync(t => t.Id == id);
                _context.Technos.Remove(oldTechno);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Get(bool sort_by_name)
        {
            try
            {
                var sort = sort_by_name ? "techno_name asc":"created_at desc";
                var userId = HttpContext.Items["id"];
                var commandText = "SELECT * FROM technos WHERE user_id=" + userId + " order by " + sort;
                var result = await _context.Set<Techno>().FromSqlRaw(commandText).ToListAsync();
                var nresult = MappingContext.technosToTechnoMaps(result);
                return Ok(nresult);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

        [HttpPost("search")]
        public async Task<ActionResult> search(bool sort_by_name, int search, TechnoMap techno0_)
        {
            try
            {
                var techno_ = techno0_.generateTechno();
                Func<bool?, string> strStatus = x => {
                    var dictStatus = new Dictionary<string, string> { [""] = "null", ["False"] = "0", ["True"] = "1" };
                    return dictStatus[x.ToString()];
                };
                var sort = sort_by_name ? "techno_name asc" : "created_at desc";
                
                Console.WriteLine("filter method: " + new String[] { "contains", "start with", "end with", "exact" }[search - 1]);
                var userId = Convert.ToInt32(HttpContext.Items["id"]);
                var commandText = $"SELECT * FROM technos WHERE user_id={userId} and (techno_name='{techno_.TechnoName}' or ''='{techno_.TechnoName}') and (techno_status={strStatus(techno_.TechnoStatus)} or {strStatus(techno_.TechnoStatus)} is null) order by {sort}";
                if (search == 1)
                {
                    commandText = $"SELECT * FROM technos WHERE user_id={userId} and (techno_name like '%{techno_.TechnoName}%' or ''='{techno_.TechnoName}') and (techno_status={strStatus(techno_.TechnoStatus)} or {strStatus(techno_.TechnoStatus)} is null) order by {sort}";
                }
                else if (search == 2)
                {
                    commandText = $"SELECT * FROM technos WHERE user_id={userId} and (techno_name like '{techno_.TechnoName}%' or ''='{techno_.TechnoName}') and (techno_status={strStatus(techno_.TechnoStatus)} or {strStatus(techno_.TechnoStatus)} is null) order by {sort}";
                }
                else if (search == 3)
                {
                    commandText = $"SELECT * FROM technos WHERE user_id={userId} and (techno_name like '%{techno_.TechnoName}' or ''='{techno_.TechnoName}') and (techno_status={strStatus(techno_.TechnoStatus)} or {strStatus(techno_.TechnoStatus)} is null) order by {sort}";
                }
                var result = await _context.Set<Techno>().FromSqlRaw(commandText).ToListAsync();
                var nresult = MappingContext.technosToTechnoMaps(result); 
                return Ok(nresult);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }

        }
    }
}
