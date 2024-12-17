using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vault_IssueTracker.Model;

namespace Vault_IssueTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ModelContext _dbContext;
    public UserController(ModelContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        // Fetch all the existing users in the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            return await _dbContext.Users.ToListAsync();
        }

        // Fetch a specific user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // Post; See if the inputs will reflect
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _dbContext.Users.Add(user); 
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new {id = user.id}, user);
        }

        // Put; Update an existing user
        [HttpPut]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if(id != user.id)
            {
                return BadRequest();
            }
            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) 
            { 
                if(!UserAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        // Check if the report is in the database
        private bool UserAvailable(int id)
        {
            return (_dbContext.Users?.Any(x => x.id == id)).GetValueOrDefault();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
