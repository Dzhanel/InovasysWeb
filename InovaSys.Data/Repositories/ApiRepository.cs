using System.Net.Http.Json;
using Inovasys.Data.Dto;
using Inovasys.Data.Entites;
using Inovasys.Data.Interfaces;

namespace Inovasys.Data.Repositories
{
    public class ApiRepository : IApiUserRepository
    {
        private readonly HttpClient http;
        private const string apiUrl = "https://jsonplaceholder.typicode.com/users";
        public ApiRepository(HttpClient _http)
        {
            http = _http;
        }

        public async Task<List<User>> FetchUsersAsync()
        {
            var users = await http.GetFromJsonAsync<List<ApiUserDto>>(apiUrl) ?? [];
            return users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Username = u.Username,
                Email = u.Email,
                Phone = u.Phone,
                Website = u.Website,
                IsActive = false,
                Address = new Address
                {
                    Street = u.Address.Street,
                    Suite = u.Address.Suite,
                    City = u.Address.City,
                    Zipcode = u.Address.Zipcode,
                    Latitude = (double)u.Address.Geo.Lat,
                    Longitude = (double)u.Address.Geo.Lng
                },
                Password = string.Empty,
                Note = string.Empty,
            }).ToList();


        }
    }
}
