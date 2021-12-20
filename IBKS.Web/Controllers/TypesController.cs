using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Type = IBKS.Web.Models.Type;

namespace IBKS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IBKSDbContext myDbContext;

        public TypesController(IBKSDbContext context)
        {
            myDbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type>>> GetTypes()
        {
            return await myDbContext.Types.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Type>> GetType(int id)
        {
            var @type = await myDbContext.Types.FindAsync(id);

            if (@type == null)
            {
                return NotFound();
            }

            return @type;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(int id, Type @type)
        {
            if (id != @type.Id)
            {
                return BadRequest();
            }

            myDbContext.Entry(@type).State = EntityState.Modified;

            try
            {
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Type>> PostType(Type @type)
        {
            myDbContext.Types.Add(@type);
            await myDbContext.SaveChangesAsync();

            return CreatedAtAction("GetType", new { id = @type.Id }, @type);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Type>> DeleteType(int id)
        {
            var @type = await myDbContext.Types.FindAsync(id);
            if (@type == null)
            {
                return NotFound();
            }

            myDbContext.Types.Remove(@type);
            await myDbContext.SaveChangesAsync();

            return @type;
        }

        private bool TypeExists(int id)
        {
            return myDbContext.Types.Any(e => e.Id == id);
        }
    }
}
