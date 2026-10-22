using System.ComponentModel.DataAnnotations;

namespace BloodCareF.Models
{
    public class BloodRequest
    {
        public int Id { get; set; }
        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [Required]
        public int BloodTypeId { get; set; }
        public BloodType BloodType { get; set; }
        [Range(1,10)]
        public int QuantityNeeded { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
