namespace SMSChat.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SMSChat.Data;
    using SMSChat.Models;
 

    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FriendsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Friend>>> GetFriends([FromQuery] string phoneNumber)
        {
            return await _context.Friends
                .Where(f => f.FriendSipPhoneNumber == phoneNumber)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend([FromBody] Friend friend)
        {
            _context.Friends.Add(friend);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFriends), new { phoneNumber = friend.FriendSipPhoneNumber }, friend);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFriend(int id, [FromBody] Friend friend)
        {
            if (id != friend.Id)
            {
                return BadRequest();
            }

            _context.Entry(friend).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
