using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IBKS.Web.Models;

namespace IBKS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        private readonly IBKSDbContext myDbContext;

        public LightsController(IBKSDbContext context)
        {
            myDbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Light>>> GetLights()
        {
            return await myDbContext.Lights.Include(x => x.Room).Include(x => x.Type).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Light>> GetLight(int id)
        {
            var light = await myDbContext.Lights.FindAsync(id);

            if (light == null)
            {
                return NotFound();
            }

            return light;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Light>> PutLight(int id, Light light)
        {
            if (id != light.Id)
            {
                return BadRequest();
            }

            myDbContext.Entry(light).State = EntityState.Modified;

            try
            {
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await myDbContext.Lights
                .Include(x => x.Room)
                .Include(x => x.Type)
                .Where(x => x.Id == light.Id)
                .FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Light>> PostLight(Light light)
        {
            myDbContext.Lights.Add(light);
            await myDbContext.SaveChangesAsync();
           
            light = await myDbContext.Lights
                .Include(x => x.Room)
                .Include(x => x.Type)
                .Where(x => x.Id == light.Id)
                .FirstOrDefaultAsync();

            return CreatedAtAction("GetLight", new { id = light.Id }, light);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Light>> DeleteLight(int id)
        {
            var light = await myDbContext.Lights.FindAsync(id);
            if (light == null)
            {
                return NotFound();
            }

            myDbContext.Lights.Remove(light);
            await myDbContext.SaveChangesAsync();

            return light;
        }

        private bool LightExists(int id)
        {
            return myDbContext.Lights.Any(e => e.Id == id);
        }
    }
}
