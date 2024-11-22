using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10045251_PROTOTYPE.Models;
using ST10045251_PROTOTYPE.Services;
using System;
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

        public IActionResult ClaimStatus()
        {
            var claims = new List<Claim>
        {
            new Claim { ClaimId = 1, HoursWorked = 8, Status = "Pending" },
            new Claim { ClaimId = 2, HoursWorked = 10, Status = "Approved" }
        };

            return View(claims);
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

        [AllowAnonymous]
        public IActionResult Submit()
        {
            return View("~/Views/Account/SubmitForm.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.Amount = claim.HoursWorked * claim.HourlyRate;
                claim.Status = "Pending";
                claim.SubmissionDate = DateTime.UtcNow;

                _context.Claims.Add(claim); 
                await _context.SaveChangesAsync();

                return RedirectToAction("SubmissionConf", new { id = claim.ClaimId });
            }

            return View(claim);
        }

        public async Task<IActionResult> SubmissionConf(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }
    }
}
