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
    public class RoomsController : ControllerBase
    {
        private readonly IBKSDbContext myDbContext;

        public RoomsController(IBKSDbContext context)
        {
            myDbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await myDbContext.Rooms.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await myDbContext.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            myDbContext.Entry(room).State = EntityState.Modified;

            try
            {
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            myDbContext.Rooms.Add(room);
            await myDbContext.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            var room = await myDbContext.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            myDbContext.Rooms.Remove(room);
            await myDbContext.SaveChangesAsync();

            return room;
        }

        private bool RoomExists(int id)
        {
            return myDbContext.Rooms.Any(e => e.Id == id);
        }
    }
}
