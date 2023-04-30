using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Domain.Shared;

namespace CarRental.Domain.Entities
{
    public class Cars: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Model { get; set; }
        public float Price { get; set; }
        public string NumberPlate { get; set; }
        public string Color { get; set; }
        public string PhotoUrl { get; set;  }
    }
}
