using CardinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CardinalProject.Helpers;
using CardinalProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace CardinalProject.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return View(services);
        }

        [Authorize(Roles = "4")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "4")]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string user = User.GetUserName();

            await _serviceService.AddServiceAsync(model, user);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "4")]
        public async Task<IActionResult> Edit(int id)
        {
            var serviceVm = await _serviceService.GetServiceByIdAsync(id);
            if (serviceVm == null) return NotFound();

            return View(serviceVm);
        }

        [Authorize(Roles = "4")]
        [HttpPost]
        public async Task<IActionResult> Edit(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string user = User.GetUserName();

            await _serviceService.UpdateServiceAsync(model, user);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "4")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceVm = await _serviceService.GetServiceByIdAsync(id);
            if (serviceVm == null) return NotFound();

            return View(serviceVm);
        }

        [Authorize(Roles = "4")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
