namespace ST10045251_PROTOTYPE.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string ClaimantName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Notes { get; set; }
    }
}
