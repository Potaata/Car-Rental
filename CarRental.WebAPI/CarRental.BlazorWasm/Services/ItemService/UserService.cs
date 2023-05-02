using CarRental.BlazorWasm.Models;
using CarRental.BlazorWasm.Models.Users;
using CarRental.BlazorWasm.Models.Items;
using CarRental.BlazorWasm.Models.Register;

namespace CarRental.BlazorWasm.Services.ItemService
{
    public class UserService
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
        public async Task<List<User>> GetRegularUsers()
        {
            UserResponse users = await _apiService.GET<UserResponse>(EndPoint + "/regular");

            return users.users;
        }

        public async Task<List<User>> GetInactiveUsers()
        {
            UserResponse users = await _apiService.GET<UserResponse>(EndPoint + "/inactive");

            return users.users;
        }

        public async Task<string> UpdatePhotoUrl(string PhotoUrl)
        {
            MessageResponse message = await _apiService.POST<UrlResponse, MessageResponse>("/api/users/document", new UrlResponse { Url = PhotoUrl });
            return message.message;
        }

        public async Task<string> RegisterStaff(string username, string phoneNumber, string email, string password, string address, string name)
        {
            RegisterRequest req = new RegisterRequest { Name = name, Address = address, Username = username, Email = email, RawPassword = password, PhoneNumber = phoneNumber };
            MessageResponse res = await _apiService.POST<RegisterRequest, MessageResponse>("/api/admin/add-staff", req);
            return res.message;
        }

        public async Task<string> RegisterAdmin(string username, string phoneNumber, string email, string password, string address, string name)
        {
            RegisterRequest req = new RegisterRequest { Name = name, Address = address, Username = username, Email = email, RawPassword = password, PhoneNumber = phoneNumber };
            MessageResponse res = await _apiService.POST<RegisterRequest, MessageResponse>("/api/admin/add-admin", req);
            return res.message;
        }
        

        public UserRequest GetDefaultRequest()
        {
            return new UserRequest();
        }
    }
}
