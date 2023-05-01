using CarRental.Application.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Domain.Entities;
using CarRental.Application.DTOs.CarDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarRental.Infrastructure.Exceptions;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Infrastructure.Services
{
    public class CarServices: ICars
    {
        private readonly IApplicationDBContext _dbcontext;
        private readonly IAuthService _authService;

        public CarServices(IApplicationDBContext dbContext, IAuthService authService)
        {
            _dbcontext = dbContext;
            _authService = authService;
        }

        public async Task<ListCarResponse> GetCars()
        {
            List<Cars> allCars = await _dbcontext.Cars.ToListAsync();
            ListCarResponse response = new ListCarResponse
            {
                cars = allCars
            };
            return response;
        }

        public async Task<SingleCarResponse> GetCarById(int id)
        {
            Cars car = await _dbcontext.Cars.FindAsync(id);

            if (car == null)
            {
                throw new ApiException("Invalid ID!");
            }

            SingleCarResponse response = new SingleCarResponse
            {
                car = car
            };

            return response;
        }

        public async Task<MessageResponse> AddCar(AddCarRequestDTO car)
        {
            Cars newCar = new Cars
            {
                Model = car.Model,
                Price = car.Price,
                NumberPlate = car.NumberPlate,
                Color = car.Color,
                PhotoUrl = car.PhotoUrl
            };
                
            _dbcontext.Cars.Add(newCar);
            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Car added successfully." };
        }

        public async Task<MessageResponse> UpdateCar(int id, AddCarRequestDTO car)
        {
            await _authService.GetSessionUser(new List<string>{ "Admin"});
            Cars carToUpdate = await _dbcontext.Cars.FindAsync(id);

            if (carToUpdate == null)
            {
                throw new ApiException("Invalid ID!");
            }

            carToUpdate.Model = car.Model;
            carToUpdate.Price = car.Price;
            carToUpdate.NumberPlate = car.NumberPlate;
            carToUpdate.Color = car.Color;

            _dbcontext.Cars.Update(carToUpdate);
            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Car updated successfully." };
        }

        public async Task<MessageResponse> DeleteCar(int id)
        {
            Cars carToDelete = await _dbcontext.Cars.FindAsync(id);

            if (carToDelete == null)
            {
                throw new ApiException("Invalid ID!");
            }

            _dbcontext.Cars.Remove(carToDelete);
            await _dbcontext.SaveChangesAsync();

            return new MessageResponse { message = "Car deleted successfully." };
        }
    }
}
