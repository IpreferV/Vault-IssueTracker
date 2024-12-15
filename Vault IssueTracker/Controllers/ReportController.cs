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
            _dbContext = _dbContext;
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

        [HttpGet]
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
    }
}
