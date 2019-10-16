using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmigoLoansProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AmigoLoansProject.Controllers
{
    [Route("api/[controller]")]
    public class EngineerController : Controller
    {
        private readonly EngineerContext _context;

        public EngineerController(EngineerContext context)
        {
            _context = context;

            if (_context.ListOfEngineers.Count() == 0)
            {
                _context.ListOfEngineers.Add(new Engineer { Name = "Jenny Baker", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "Finn Davis", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "Alex Goodman", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "Trever Deely", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "John Blogs", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "Mary Monroe", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "Sarah Silver", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "Fred Pear", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "Pete Samson", ShiftCount = 0 });
                _context.ListOfEngineers.Add(new Engineer { Name = "Michael Moore", ShiftCount = 0 });
                _context.SaveChanges();
            }
        }

        // GET: api/Engineer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Engineer>>> ListEngineers()
        {
            return await _context.ListOfEngineers.ToListAsync();
        }

        // GET: api/Engineer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Engineer>> GetEngineer(long id)
        {
            var engineer = await _context.ListOfEngineers.FindAsync(id);

            if (engineer == null)
            {
                return NotFound();
            }

            return engineer;
        }

        // GET: api/Engineer/getRandomEngineers/{workDay}
        [HttpGet("getRandomEngineers/{workDay}")]
        public async Task<ActionResult<IEnumerable<Engineer>>> GetRandomEngineers(DateTime workDay)
        {
            List<Engineer> resultList = null;
            //TODO - Check for longer than 2 week period and reset shift
            var engineersNotWorked2Shifts = await _context.ListOfEngineers
                                            .Where(e => e.ShiftCount < 2)
                                                .ToListAsync();

            var engineersAvailable = engineersNotWorked2Shifts
                                        .Where(e => e.LastWorked.HasValue ? (workDay - (DateTime)e.LastWorked).Days > 1 : true)
                                            .ToList();

            var listCount = engineersAvailable.Count();

            if (listCount != 0)
            {
                resultList = new List<Engineer>();
                Random random = new Random();

                for (int i = 0; i < 2; i++)
                {
                    var randomEngineer = engineersAvailable[random.Next(listCount)];

                    resultList.Add(randomEngineer);
                    engineersAvailable.Remove(randomEngineer);
                    listCount -= 1;
                }
            }

            if (resultList == null)
            {
                return NotFound();
            }

            return resultList;
        }

        // POST: api/Engineer
        [HttpPost]
        public async Task<ActionResult<Engineer>> PostEngineer(Engineer item)
        {
            _context.ListOfEngineers.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEngineer), new { id = item.Id }, item);
        }

        // PUT: api/Engineer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEngineer(long id, [FromBody]Engineer engineer)
        {
            if (id != engineer.Id)
            {
                return BadRequest();
            }

            _context.Entry(engineer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Engineer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngineer(long id)
        {
            var engineer = await _context.ListOfEngineers.FindAsync(id);

            if (engineer == null)
            {
                return NotFound();
            }

            _context.ListOfEngineers.Remove(engineer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
