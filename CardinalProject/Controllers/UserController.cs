using CardinalProject.Helpers;
using CardinalProject.Models;
using CardinalProject.Services;
using CardinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardinalProject.Controllers
{
    [Authorize(Roles = "2,3,4")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHospitalService _hospitalService;
        private readonly IUserRoleService _roleService;

        public UserController(IUserService userService, IHospitalService hospitalService, IUserRoleService roleService)
        {
            _userService = userService;
            _hospitalService = hospitalService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roleId = User.GetRoleId();
            var hospitalId = User.GetHospitalId();

            IEnumerable<UserViewModel> users;

            if (roleId == 4)
                users = await _userService.GetAllUsersAsync();
            else if (roleId == 3)
                users = await _userService.GetAllUsersInHospitalAsync(hospitalId ?? 0);
            else if (roleId == 2)
                users = await _userService.GetAllUsersInHospitalMinusAdminAsync(hospitalId ?? 0);
            else
                return Forbid();

            return View(users);
        }

        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var currentUserId = User.GetUserId(); 
            ViewData["CurrentUserId"] = currentUserId;

            return View("Details", user);
        }

        public async Task<IActionResult> CreateUser()
        {
            var model = new UserViewModel
            {
                IsActive = true
            };

            await PopulateDropdownsAsync(model);
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            Console.WriteLine(ModelState);
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    var key = modelState.Key;
                    var errors = modelState.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
                await PopulateDropdownsAsync(model);
                return View("Create", model);
            }
            Console.WriteLine("No model error");
            var user = new User
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Department = model.Department,
                HospitalId = model.HospitalId,
                RoleId = model.RoleId,
                Email = model.Email,
                IsActive = model.IsActive,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Default@123")
            };

            await _userService.AddUserAsync(user, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditUser(int id)
        {
            var currentUserId = User.GetUserId();
            if (id == currentUserId)
                return Forbid();

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            await PopulateDropdownsAsync(user, user.HospitalId, user.RoleId);
            return View("Edit", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();

            var currentUserId = User.GetUserId();
            if (id == currentUserId)
                return Forbid(); // Prevent self-edit

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model, model.HospitalId, model.RoleId);
                return View("Edit", model);
            }

            var originalUser = await _userService.GetUserEntityByIdAsync(id);
            if (originalUser == null) return NotFound();

            // Update only the fields that are editable
            originalUser.Name = model.Name;
            originalUser.PhoneNumber = model.PhoneNumber;
            originalUser.Department = model.Department;
            originalUser.HospitalId = model.HospitalId;
            originalUser.RoleId = model.RoleId;
            originalUser.Email = model.Email;
            originalUser.IsActive = model.IsActive;
            originalUser.UpdatedAt = DateTime.UtcNow;
            originalUser.UpdatedBy = User.Identity.Name;

            await _userService.UpdateUserAsync(originalUser, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ConfirmDeleteUser(int id)
        {
            var currentUserId = User.GetUserId();
            if (id == currentUserId)
                return Forbid();

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            return View("Delete", user);
        }

        [HttpPost, ActionName("ConfirmDeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteUserPost(int id)
        {
            var currentUserId = User.GetUserId();
            if (id == currentUserId)
                return Forbid();


            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownsAsync(UserViewModel model, int? selectedHospitalId = null, int? selectedRoleId = null)
        {
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            var roles = await _roleService.GetAllUserRolesAsync();

            int currentUserRoleId = User.GetRoleId(); 

            // Only allow assigning roles strictly below the current user's role
            var assignableRoles = roles.Where(r => r.Id < currentUserRoleId).ToList();

            model.HospitalSelectList = new SelectList(hospitals, "Id", "Name", selectedHospitalId ?? model.HospitalId);
            model.RoleSelectList = new SelectList(assignableRoles, "Id", "Name", selectedRoleId ?? model.RoleId);
        }


    }
}
