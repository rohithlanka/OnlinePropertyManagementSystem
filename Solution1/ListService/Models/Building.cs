using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListService.Models
{
    public class Building
    {
        [Key]
        public int building_id { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public double cost { get; set; }
    }
}
