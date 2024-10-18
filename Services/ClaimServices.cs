namespace ST10045251_PROTOTYPE.Services
{
    public class ClaimServices
    {
        private readonly ApplicationDbContext _context;

        public ClaimServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ApproveClaim(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = "Approved";
                await _context.SaveChangesAsync();
            }
        }
    }
}