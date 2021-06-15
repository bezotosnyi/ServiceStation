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
    public class OrdersController : Controller
    {
        private readonly OrdersService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(OrdersService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetItems());
        }

        public async Task<IActionResult> Completed()
        {
            return View(await _orderService.GetItems());
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
                var item = new OrderDTO
                {
                    Date = Convert.ToDateTime(collection["Date"]),
                    ServiceName = collection["ServiceName"],
                    Price = Convert.ToInt32(collection["Price"]),
                    CarId = Convert.ToInt32(collection["CarId"]),
                    OwnerId = Convert.ToInt32(collection["OwnerId"]),
                    InspectorId = Convert.ToInt32(collection["InspectorId"]),
                    IsCompleted = false
                };
                await _orderService.Create(item);
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
            return View(await _orderService.GetItem(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var item = new OrderDTO
                {
                    Id = id,
                    Date = Convert.ToDateTime(collection["Date"]),
                    ServiceName = collection["ServiceName"],
                    Price = Convert.ToInt32(collection["Price"]),
                    CarId = Convert.ToInt32(collection["CarId"]),
                    OwnerId = Convert.ToInt32(collection["OwnerId"]),
                    InspectorId = Convert.ToInt32(collection["InspectorId"])
                };
                await _orderService.Update(item);
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
            return View(await _orderService.GetItem(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _orderService.Delete(id);
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
            return View(await _orderService.GetItem(id));
        }

        public async Task<IActionResult> Executing(int id)
        {
            var item = await _orderService.GetItem(id);
            if (item.IsCompleted == false)
            {
                item.IsCompleted = true;
            }
            else
            {
                item.IsCompleted = false;
            }
            await _orderService.Update(item);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
