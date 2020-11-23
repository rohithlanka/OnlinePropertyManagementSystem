using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddBuildingService.Models
{
    public class Building
    {
        [Key]
        
        public int building_id { get; set; }
        [Required(ErrorMessage ="description is required")]
        public string description { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public double cost { get; set; }
    }
}
