using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10045251_PROTOTYPE.Models;
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

        // View Pending Claims
        public IActionResult PendingClaims()
        {
            var pendingClaims = _context.Claims.Where(c => c.Status == "Pending").ToList();
            return View(pendingClaims);
        }

        // Approve a Claim
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

        // Reject a Claim
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

        // Display the Claim Submission Form
        public IActionResult Submit()
        {
            return View();
        }

        // Handle Claim Submission
        [HttpPost]
        public async Task<IActionResult> Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                // Automate the calculation of the claim amount
                claim.Amount = claim.HoursWorked * claim.HourlyRate;
                claim.Status = "Pending"; // Default status
                claim.SubmissionDate = DateTime.UtcNow;

                _context.Claims.Add(claim); // Save the claim to the database
                await _context.SaveChangesAsync();

                return RedirectToAction("SubmissionConfirmation", new { id = claim.ClaimId });
            }

            return View(claim); // Return to the form if validation fails
        }

        // Display Confirmation Page after Submission
        public async Task<IActionResult> SubmissionConfirmation(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim); // Show confirmation details
        }
    }
}
