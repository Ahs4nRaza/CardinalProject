using CardinalProject.Services;
using CardinalProject.ViewModels;
using CardinalProject.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardinalProject.Controllers
{
    [Authorize]
    public class ServiceProviderController : Controller
    {
        private readonly IServiceProviderService _serviceProviderService;

        public ServiceProviderController(IServiceProviderService serviceProviderService)
        {
            _serviceProviderService = serviceProviderService;
        }

        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var providers = await _serviceProviderService.GetAllServiceProvidersAsync();
            return View(providers);
        }

        
        [Authorize(Roles = "3,4")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "3,4")]
        [HttpPost]
        public async Task<IActionResult> Create(ServiceProviderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string createdBy = User.GetUserName();
            await _serviceProviderService.AddServiceProviderAsync(model, createdBy);

            return RedirectToAction(nameof(Index));
        }

        
        [Authorize(Roles = "2,3,4")]
        public async Task<IActionResult> Edit(int id)
        {
            var provider = await _serviceProviderService.GetServiceProviderByIdAsync(id);
            if (provider == null)
                return NotFound();

            return View(provider);
        }

        [Authorize(Roles = "2,3,4")]
        [HttpPost]
        public async Task<IActionResult> Edit(ServiceProviderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string updatedBy = User.GetUserName();
            await _serviceProviderService.UpdateServiceProviderAsync(model, updatedBy);

            return RedirectToAction(nameof(Index));
        }

        
        [Authorize(Roles = "3,4")]
        public async Task<IActionResult> Delete(int id)
        {
            var provider = await _serviceProviderService.GetServiceProviderByIdAsync(id);
            if (provider == null)
                return NotFound();

            return View(provider);
        }

        [Authorize(Roles = "3,4")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _serviceProviderService.DeleteServiceProviderAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
