using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Users;
using CarRental.BlazorWasm.Models.Items;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class UserService : IItemService<User, UserRequest>
    {
        private readonly ApiService _apiService;

        public string EndPoint = "/api/users";

        public UserService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<User>> GetItems()
        {
            UserResponse users = await _apiService.GET<UserResponse>(EndPoint);

            return users.users;
        }

        public async Task<string> DeleteItem(int id)
        {
            MessageResponse message = await _apiService.DELETE<MessageResponse>(EndPoint + '/' + id);

            return message.message;
        }
        public async Task<string> CreateItem(UserRequest user)
        {
            MessageResponse message = await _apiService.POST<UserRequest, MessageResponse>(EndPoint, user);

            return message.message;
        }

        public async Task<string> EditItem(int id, UserRequest user)
        {
            MessageResponse message = await _apiService.PUT<UserRequest, MessageResponse>(EndPoint + '/' + id, user);

            return message.message;
        }

        public UserRequest GetDefaultRequest()
        {
            return new UserRequest();
        }
    }
}
