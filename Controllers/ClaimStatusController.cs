using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10045251_PROTOTYPE.Models; 
using System.Linq;
using System.Threading.Tasks;

namespace ST10045251_PROTOTYPE.Controllers
{
    [Authorize]
    public class ClaimStatusController : Controller 
    {
        private readonly ApplicationDbContext _context;

        public ClaimStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult PendingClaims()
        {
            var pendingClaims = _context.Claims.Where(c => c.Status == "Pending").ToList();
            return View(pendingClaims);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = "Approved";
                _context.Update(claim);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = "Rejected";
                _context.Update(claim);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("PendingClaims");
        }
    }
}
