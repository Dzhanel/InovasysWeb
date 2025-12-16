using Inovasys.Data.Entites;
using Inovasys.Data.Interfaces;
using Inovasys.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Inovasys.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IApiUserRepository apiRepository;
        private readonly IUserUserRepository userRepository;
        
        public UsersController(IApiUserRepository _apiUserRepository, IUserUserRepository _userUserRepository)
        {
            apiRepository = _apiUserRepository;
            userRepository = _userUserRepository;
        }

        public async Task<IActionResult> Index()
        {
            int dbUserCount = await userRepository.UserCount();

            List<User> usersEntity = dbUserCount == 0
                ? await apiRepository.FetchUsersAsync()
                : [.. await userRepository.GetAllAsync()];

            return View(usersEntity.Select(MapToViewModel).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReloadFromApi()
        {
            try
            {
                var usersFromApi = await apiRepository.FetchUsersAsync();

                await userRepository.ReplaceAllAsync(usersFromApi);

                TempData["Success"] = $"Successfully reloaded users from API. Please set passwords for all users.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to reload data from API: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAll(Dictionary<int, SaveUserViewModel> users)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data. Please check all fields and try again.";
                return View("Index", users.Values.Select(MapSaveViewModelToViewModel).ToList());
            }

            try
            {
                var userEntities = users.Values.Select(MapSaveViewModelToEntity).ToList();
                var success = await userRepository.ReplaceAllAsync(userEntities);

                TempData[success ? "Success" : "Warning"] = success 
                    ? $"Successfully saved {userEntities.Count} users." 
                    : "No changes were made.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to save users: {ex.Message}";
                return View("Index", users.Values.Select(MapSaveViewModelToViewModel).ToList());
            }
        }


        //TODO: Reimplement with automaper later
        private UserViewModel MapToViewModel(User entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Username = entity.Username,
            Email = entity.Email,
            Phone = entity.Phone,
            Website = entity.Website,
            IsActive = entity.IsActive,
            Note = entity.Note,
            Password = entity.Password ?? string.Empty,
            Address = new AddressViewModel
            {
                Street = entity.Address!.Street,
                Suite = entity.Address.Suite,
                City = entity.Address.City,
                Zipcode = entity.Address.Zipcode,
                Lat = entity.Address.Latitude,
                Lng = entity.Address.Longitude
            }
        };

        private UserViewModel MapSaveViewModelToViewModel(SaveUserViewModel vm) => new()
        {
            Id = vm.Id,
            Name = vm.Name,
            Username = vm.Username,
            Email = vm.Email,
            Phone = vm.Phone,
            Website = vm.Website,
            IsActive = vm.IsActive,
            Note = vm.Note,
            Password = vm.Password ?? string.Empty,
            Address = new AddressViewModel
            {
                Street = vm.Address!.Street,
                Suite = vm.Address.Suite,
                City = vm.Address.City,
                Zipcode = vm.Address.Zipcode,
                Lat = vm.Address.Lat,
                Lng = vm.Address.Lng
            }
        };

        private User MapSaveViewModelToEntity(SaveUserViewModel vm) => new()
        {
            Id = vm.Id,
            Name = vm.Name,
            Username = vm.Username,
            Email = vm.Email,
            Phone = vm.Phone ?? string.Empty,
            Website = vm.Website ?? string.Empty,
            IsActive = vm.IsActive,
            Note = vm.Note ?? string.Empty,
            Password = vm.Password,
            Address = new Address
                {
                    Street = vm.Address.Street ?? string.Empty,
                    Suite = vm.Address.Suite ?? string.Empty,
                    City = vm.Address.City ?? string.Empty,
                    Zipcode = vm.Address.Zipcode ?? string.Empty,
                    Latitude = vm.Address.Lat,
                    Longitude = vm.Address.Lng
                }
        };
    }
}
