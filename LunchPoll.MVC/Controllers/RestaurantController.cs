using LunchPoll.MVC.Models.Dtos;
using LunchPoll.MVC.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LunchPoll.MVC.Controllers
{
    public class RestaurantController(IRestaurantService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new List<RestaurantDto>();
            var responseDto = await service.GetAllRestaurantsAsync();
            if (responseDto.IsSuccess)
                model = JsonConvert.DeserializeObject<List<RestaurantDto>>(Convert.ToString(responseDto.Result));
            else
                TempData["error"] = responseDto.ErrorMessage;

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var model = new RestaurantDto();

            var responseDto = await service.GetRestaurantByIdAsync(id);
            if (responseDto.IsSuccess)
                model = JsonConvert.DeserializeObject<RestaurantDto>(Convert.ToString(responseDto.Result));
            else
                TempData["error"] = responseDto.ErrorMessage;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestaurantDto RestaurantDto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await service.CreateRestaurantAsync(RestaurantDto);
                if (responseDto.IsSuccess)
                {
                    TempData["success"] = "Restaurant created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = responseDto.ErrorMessage;
                }
            }
            return View(RestaurantDto);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await GetRestaurantById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantDto RestaurantDto)
        {
            var model = new RestaurantDto();
            var responseDto = await service.UpdateRestaurantAsync(RestaurantDto);
            if (responseDto.IsSuccess)
                return RedirectToAction(nameof(Index));
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var model = await GetRestaurantById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            var responseDto = await service.DeleteRestaurantAsync(id);
            if (responseDto.IsSuccess)
                return RedirectToAction(nameof(Index));
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
                return View();
            }
        }


        private async Task<RestaurantDto> GetRestaurantById(string id)
        {
            var response = await service.GetRestaurantByIdAsync(id);
            if (!response.IsSuccess)
            {
                TempData["error"] = response.ErrorMessage;
                return null; // Return an empty CouponDto if not found
            }
            return JsonConvert.DeserializeObject<RestaurantDto>(Convert.ToString(response.Result));
        }
    }
}
