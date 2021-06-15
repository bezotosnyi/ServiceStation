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
    public class InspectorsController : Controller
    {
        private readonly InspectorsService _inspectorService;
        private readonly ILogger<InspectorsController> _logger;

        public InspectorsController(InspectorsService inspectorService, ILogger<InspectorsController> logger)
        {
            _inspectorService = inspectorService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _inspectorService.GetItems());
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
                var inspector = new InspectorDTO
                {
                    Firstname = collection["Firstname"],
                    LastName = collection["LastName"],
                    MiddleName = collection["MiddleName"],
                    Position = collection["Position"],
                    Salary = Convert.ToDouble(collection["Salary"])
                };
                await _inspectorService.Create(inspector);
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
            return View(await _inspectorService.GetItem(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var owner = new InspectorDTO
                {
                    Id = id,
                    Firstname = collection["Firstname"],
                    LastName = collection["LastName"],
                    MiddleName = collection["MiddleName"],
                    Position = collection["Position"],
                    Salary = Convert.ToDouble(collection["Salary"])
                };
                await _inspectorService.Update(owner);
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
            return View(await _inspectorService.GetItem(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _inspectorService.Delete(id);
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
            return View(await _inspectorService.GetItem(id));
        }
    }
}
