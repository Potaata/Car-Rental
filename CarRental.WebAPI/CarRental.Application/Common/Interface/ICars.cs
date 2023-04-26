using CarRental.Application.DTOs.CarDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Common.Interface
{
    public interface ICars
    {
        public Task<ListCarResponse> GetCars();
        public Task<SingleCarResponse> GetCarById(int carId);
        public Task<MessageResponse> UpdateCar(int id, AddCarRequestDTO car);
        public Task<MessageResponse> DeleteCar(int id);
        public Task<MessageResponse> AddCar(AddCarRequestDTO car);
    }
}
