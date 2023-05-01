using CarRental.Application.DTOs.CarDTOs;
using CarRental.Application.DTOs.RentHistoryDTOs;
using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Common.Interface
{
    public interface IRentHistory
    {
        public Task<MessageResponse> AddRentHistory(RentHistory rentHistory);

        public Task<MessageResponse> ApproveRequest(int rentId);

        public Task<MessageResponse> DeclineRequest(int rentId);
        
        public Task<List<RentHistoryResponseDTO>> GetRentHistories();

      //  public Task<RentHistory> GetRentHistoryById(int id);

        public Task<List<RentHistory>> GetRentHistoriesByUserId(string id);

        public Task<MessageResponse> CarTaken(int rentId);

        public Task<MessageResponse> CancelCarRequest(int rentId);
    }
}
