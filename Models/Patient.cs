using System.ComponentModel.DataAnnotations;

namespace BloodCareF.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int BloodTypeID { get; set; }
        public BloodType BloodType { get; set; }
        public string Phone {  get; set; }
    }
}
