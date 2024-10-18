namespace ST10045251_PROTOTYPE.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string ClaimantName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        
    }
}