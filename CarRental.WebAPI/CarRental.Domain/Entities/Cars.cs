using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Entities
{
    public class Cars
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public float Price { get; set; }
        public string NumberPlate { get; set; }
        public string Color { get; set; }
    }
}
