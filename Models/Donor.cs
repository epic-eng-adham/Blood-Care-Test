using System.ComponentModel.DataAnnotations;

namespace BloodCareF.Models
{
    public class Donor
    {
        public int ID { get; set; }
        [Required]
        [StringLength(30)]
        public string FullName { get; set; }
        public int BloodTypeId { get; set; }
        public BloodType BloodType { get; set; }
        [Range(18,65)]
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone {  get; set; }
        public string Address {  get; set; }
        public int DonationQuantity { get; set; }
        public DateTime? LastDonationDate { get; set; }
    }
}
