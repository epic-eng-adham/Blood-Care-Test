using System.ComponentModel.DataAnnotations;

namespace BloodCareF.Models
{
    public class BloodType
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string TypeName { get; set; }
        [Range(0,10000)]
        public int AvailableQuantity { get; set; }
    }
}
