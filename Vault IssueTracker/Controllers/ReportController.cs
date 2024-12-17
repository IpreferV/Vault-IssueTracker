using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vault_IssueTracker.Model;

namespace Vault_IssueTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ModelContext _dbContext;
    public ReportController(ModelContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReport()
        {
            if (_dbContext.Reports == null)
            {
                return NotFound();
            }
            return await _dbContext.Reports.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            if (_dbContext.Reports == null)
            {
                return NotFound();
            }
            var report = await _dbContext.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }

        // Post; See if the inputs will reflect
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            _dbContext.Reports.Add(report); 
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReport), new {id = report.id}, report);
        }

        [HttpPut]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            if(id != report.id)
            {
                return BadRequest();
            }
            _dbContext.Entry(report).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) 
            { 
                if(!ReportAvailable(id))
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
        private bool ReportAvailable(int id)
        {
            return (_dbContext.Reports?.Any(x => x.id == id)).GetValueOrDefault();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            if (_dbContext.Reports == null)
            {
                return NotFound();
            }

            var report = await _dbContext.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _dbContext.Reports.Remove(report);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


    }
}
