using CardinalProject.Helpers;
using CardinalProject.Services;
using CardinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardinalProject.Controllers
{
    [Authorize(Roles = "4")]
    public class HospitalController : Controller
    {
        private readonly IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        public async Task<IActionResult> Index()
        {
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            return View(hospitals); 
        }

        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            if (hospital == null) return NotFound();
            return View(hospital); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new HospitalViewModel { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HospitalViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string name = User.GetUserName();
            await _hospitalService.AddHospitalAsync(model, name);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            if (hospital == null) return NotFound();
            return View(hospital);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HospitalViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            string name = User.GetUserName();
            await _hospitalService.UpdateHospitalAsync(model, name);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            if (hospital == null) return NotFound();
            return View(hospital);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string name = User.GetUserName();
            await _hospitalService.DeleteHospitalAsync(id, name);
            return RedirectToAction(nameof(Index));
        }
    }
}
