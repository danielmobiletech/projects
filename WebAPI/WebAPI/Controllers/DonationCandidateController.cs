using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationCandidateController : Controller
    {
        private readonly DonationDbContext _dbcontext;
        public DonationCandidateController(DonationDbContext context)
        {
            _dbcontext = context;
        }

        // GET: api/DonationCandidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonationCandidate>>> GetDonationCandidates()
        {
            return await _dbcontext.DonationCandidates.ToListAsync();
        }

        // GET: api/DonationCandidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonationCandidate>> GetDonationCandidate(int id)
        {
            var DonationCandidate = await _dbcontext.DonationCandidates.FindAsync(id);

            if (DonationCandidate == null)
            {
                return NotFound();
            }

            return DonationCandidate;
        }

        // PUT: api/DonationCandidate/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonationCandidate(int id, DonationCandidate DonationCandidate)
        {
            DonationCandidate.id = id;

            _dbcontext.Entry(DonationCandidate).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationCandidateExists(id))
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

        // POST: api/DonationCandidate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DonationCandidate>> PostDonationCandidate(DonationCandidate DonationCandidate)
        {
            _dbcontext.DonationCandidates.Add(DonationCandidate);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction("GetDonationCandidate", new { id = DonationCandidate.id }, DonationCandidate);
        }

        // DELETE: api/DonationCandidate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DonationCandidate>> DeleteDonationCandidate(int id)
        {
            var DonationCandidate = await _dbcontext.DonationCandidates.FindAsync(id);
            if (DonationCandidate == null)
            {
                return NotFound();
            }

            _dbcontext.DonationCandidates.Remove(DonationCandidate);
            await _dbcontext.SaveChangesAsync();

            return DonationCandidate;
        }

        private bool DonationCandidateExists(int id)
        {
            return _dbcontext.DonationCandidates.Any(e => e.id == id);
        }
    }
}
