using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStation.BLL.Models;
using ServiceStation.BLL.Services;

namespace ServiceStation.WebUI.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly CarsService _carService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(CarsService carService, ILogger<CarsController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _carService.GetItems());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                var car = new CarDTO
                {
                    CarNumber = collection["CarNumber"],
                    CarModel = collection["CarModel"],
                    EngineCapacity = Convert.ToDouble(collection["EngineCapacity"]),
                    BodyNumber = collection["BodyNumber"],
                    YearOfProduction = Convert.ToInt32(collection["YearOfProduction"])
                };
                await _carService.Create(car);
                _logger.LogInformation("Creation was successful.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Creation failed.", ex);
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _carService.GetItem(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var car = new CarDTO
                {
                    Id = id,
                    CarNumber = collection["CarNumber"],
                    CarModel = collection["CarModel"],
                    EngineCapacity = Convert.ToDouble(collection["EngineCapacity"]),
                    BodyNumber = collection["BodyNumber"],
                    YearOfProduction = Convert.ToInt32(collection["YearOfProduction"]),
                };
                await _carService.Update(car);
                _logger.LogInformation("Editing was successful.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Editing failed.", ex);
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _carService.GetItem(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _carService.Delete(id);
                _logger.LogInformation("Delete was successful.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete failed.", ex);
                return View();
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View(await _carService.GetItem(id));
        }
    }
}
