using CarRental.Domain.Entities;

namespace CarRental.Application.DTOs.CarDTOs
{
    public class AddCarRequestDTO
    {
        public string Model { get; set; }
        public float Price { get; set; }
        public string NumberPlate { get; set; }
        public string Color { get; set; }
    }
}
