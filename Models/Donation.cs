using System.ComponentModel.DataAnnotations;

namespace BloodCareF.Models
{
    public class Donation
    {
        public int Id { get; set; }
        [Required]
        public int DonorId {  get; set; }
        public Donor Donor { get; set; }
        public int PAtientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime DonationDate { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
    }
}
