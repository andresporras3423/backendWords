using backendWords.MappingClasses;
using backendWords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace backendWords.Controllers
{
    [Route("[controller]")]
    [CustomActionFilter]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly testContext _context;
        private Func<int?, string> strVal = x => x == null ? "null" : x.ToString();

        public WordsController(testContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(WordMap word0_)
        {
            try
            {
                var word_ = word0_.generateWord();
                var userId = HttpContext.Items["id"];
                word_.UserId = Convert.ToInt32(userId);
                _context.Words.Add(word_);
                await _context.SaveChangesAsync();
                word0_ = word_.generateWordMap();
                return Ok(word0_);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(WordMap word0_)
        {
            try
            {
                var word_ = word0_.generateWord();
                var oldWord = await _context.Words.FirstOrDefaultAsync(t => t.Id == word_.Id);
                oldWord.Word1 = word_.Word1;
                oldWord.Translation = word_.Translation;
                oldWord.TechnoId = word_.TechnoId;
                await _context.SaveChangesAsync();
                word0_ = word_.generateWordMap();
                return Ok(word0_);
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
                var oldWord = await _context.Words.FirstOrDefaultAsync(t => t.Id == id);
                _context.Words.Remove(oldWord);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            return Ok();
        }

        [HttpPost("search")]
        public async Task<ActionResult> search(bool sort_by_word, int search, WordMap word0_)
        {
            try
            {
                var word_ = word0_.generateWord();
                var sort = sort_by_word ? "word asc" : "created_at desc";
                Console.WriteLine("filter method: " + new String[] { "contains", "start with", "end with", "exact" }[search - 1]);
                var userId = Convert.ToInt32(HttpContext.Items["id"]);
                var commandText = $"SELECT * FROM words WHERE user_id={userId} and (word='{word_.Word1.ToString()}' or ''='{word_.Word1.ToString()}') and (translation='{word_.Translation.ToString()}' or '{word_.Translation.ToString()}'='') and (techno_id={word_.TechnoId.ToString()} or {word_.TechnoId.ToString()}=-1) order by {sort}";
                if (search == 1)
                {
                    commandText = $"SELECT * FROM words WHERE user_id={userId} and (word LIKE '%{word_.Word1.ToString()}%' or ''='{word_.Word1.ToString()}') and (translation LIKE '%{word_.Translation.ToString()}%' or '{word_.Translation.ToString()}'='') and (techno_id={word_.TechnoId.ToString()} or {word_.TechnoId.ToString()}=-1) order by {sort}";
                }
                else if (search == 2)
                {
                    commandText = $"SELECT * FROM words WHERE user_id={userId} and (word LIKE '{word_.Word1.ToString()}%' or ''='{word_.Word1.ToString()}') and (translation LIKE '{word_.Translation.ToString()}%' or '{word_.Translation.ToString()}'='') and (techno_id={word_.TechnoId.ToString()} or {word_.TechnoId.ToString()}=-1) order by {sort}";
                }
                else if (search == 3)
                {
                    commandText = $"SELECT * FROM words WHERE user_id={userId} and (word LIKE '%{word_.Word1.ToString()}' or ''='{word_.Word1.ToString()}') and (translation LIKE '%{word_.Translation.ToString()}' or '{word_.Translation.ToString()}'='') and (techno_id={word_.TechnoId.ToString()} or {word_.TechnoId.ToString()}=-1) order by {sort}";
                }
                var result = await _context.Set<Word>().FromSqlRaw(commandText).ToListAsync();
                var nresult = MappingContext.wordsToWordMaps(result);
                return Ok(nresult);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("next_practice")]
        public async Task<ActionResult> next_practice(int? techno_id)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Items["id"]);
                var commandText = $"select top 1 w.word, w.translation, t.techno_name from words as w inner join technos as t on w.techno_id=t.id where w.user_id={userId} and (w.techno_id={techno_id.ToString()} or {techno_id.ToString()}=-1) order by newid();";
                var result = await _context.Set<WordPractice>().FromSqlRaw(commandText).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("next_question")]
        public async Task<ActionResult> next_question(int? techno_id)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Items["id"]);
                var commandText = $@"with q1 as (
select id, word, translation, techno_id, ROW_NUMBER() OVER(partition by translation order by newid()) num
from words where user_id={userId} and (techno_id={techno_id.ToString()} or {techno_id.ToString()}= -1)
)
 select top 4 q1.word, q1.translation, t.techno_name 
            from q1 
			inner join technos as t 
			on q1.techno_id=t.id 
            where q1.num=1 order by newid();";
                var result = await _context.Set<WordPractice>().FromSqlRaw(commandText).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
