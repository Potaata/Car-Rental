using CarRental.Domain.Entities;

namespace CarRental.Application.DTOs.CarDTOs
{
    public class ListCarResponse
    {
        public List<Cars> cars { get; set; }
    }
}
