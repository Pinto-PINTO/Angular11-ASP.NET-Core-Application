using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsearAPI.Models;

namespace UsearAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly UserDetailContext _context;

        public UserDetailsController(UserDetailContext context)
        {
            _context = context;
        }

        // GET: api/UserDetails (Retrieve all records in the table)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetUserDetails()
        {
            return await _context.UserDetails.ToListAsync();  // sets to a list
        }

        // GET: api/UserDetails/5  (Get single user detail)
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserDetails(int id)
        {
            var userDetails = await _context.UserDetails.FindAsync(id);  // finds whether the id is in the db

            if (userDetails == null)
            {
                return NotFound();
            }

            return userDetails;  // if so returns it
        }

        // PUT: api/UserDetails/5     (Updates a given record)
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetails(int id, UserDetails userDetails)   // userDetails contains the updated object
        {
            if (id != userDetails.UserId)   // compares the parameter id vs id from object passed
            {
                return BadRequest();
            }

            _context.Entry(userDetails).State = EntityState.Modified; // set state of userDetails model as modified

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)   // concurrent update or delete of a record
            {
                if (!UserDetailsExists(id))  // record already deleted
                {
                    return NotFound();
                }
                else
                {
                    throw;  // exception
                }
            }

            return NoContent();   // update success
        }

        // POST: api/UserDetails   (Insert a new record)
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserDetails>> PostUserDetails(UserDetails userDetails)  // UserDetials model creates an object
        {
            _context.UserDetails.Add(userDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetails", new { id = userDetails.UserId }, userDetails);   // creates a url for newly created object (https://localhost:44342/api/userdetails/1)
        }

        // DELETE: api/UserDetails/5    (Delete Record)
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDetails>> DeleteUserDetails(int id)   // deletes the id passed
        {
            var userDetails = await _context.UserDetails.FindAsync(id);  // finds the record and saves in userDetails
            if (userDetails == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(userDetails);  // removes the record
            await _context.SaveChangesAsync();

            return userDetails;
        }

        private bool UserDetailsExists(int id)
        {
            return _context.UserDetails.Any(e => e.UserId == id);
        }
    }
}
