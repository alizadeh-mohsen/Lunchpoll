using LunchPoll.Api.Data;
using LunchPoll.MVC.Models.Dtos;
using LunchPoll.MVC.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LunchPoll.MVC.Controllers
{
    public class EmployeeController(IEmployeeService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new List<EmployeeDto>();
            var responseDto = await service.GetAllEmployeesAsync();
            if (responseDto.IsSuccess)
                model = JsonConvert.DeserializeObject<List<EmployeeDto>>(Convert.ToString(responseDto.Result));
            else
                TempData["error"] = responseDto.ErrorMessage;

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var model = new EmployeeDto();

            var responseDto = await service.GetEmployeeByIdAsync(id);
            if (responseDto.IsSuccess)
                model = JsonConvert.DeserializeObject<EmployeeDto>(Convert.ToString(responseDto.Result));
            else
                TempData["error"] = responseDto.ErrorMessage;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto EmployeeDto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await service.CreateEmployeeAsync(EmployeeDto);
                if (responseDto.IsSuccess)
                {
                    TempData["success"] = "Employee created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = responseDto.ErrorMessage;
                }
            }
            return View(EmployeeDto);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await GetEmployeeById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeDto EmployeeDto)
        {
            var model = new EmployeeDto();
            var responseDto = await service.UpdateEmployeeAsync(EmployeeDto);
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
            var model = await GetEmployeeById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            var responseDto = await service.DeleteEmployeeAsync(id);
            if (responseDto.IsSuccess)
                return RedirectToAction(nameof(Index));
            else
            {
                TempData["error"] = responseDto.ErrorMessage;
                return View();
            }
        }


        private async Task<EmployeeDto> GetEmployeeById(string id)
        {
            var response = await service.GetEmployeeByIdAsync(id);
            if (!response.IsSuccess)
            {
                TempData["error"] = response.ErrorMessage;
                return null; // Return an empty CouponDto if not found
            }
            return JsonConvert.DeserializeObject<EmployeeDto>(Convert.ToString(response.Result));
        }
    }
}
