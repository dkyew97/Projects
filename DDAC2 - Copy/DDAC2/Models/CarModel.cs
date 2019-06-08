using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC2.Models
{
    public class CarModel
    {
        [Key]
        public string CarVinNumber { get; set; }
        public string CarName { get; set; }
        public string CarMake { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        
    }
}
