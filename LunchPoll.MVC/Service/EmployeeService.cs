using LunchPoll.MVC.Helper;
using LunchPoll.MVC.Models;
using LunchPoll.MVC.Models.Dtos;
using LunchPoll.MVC.Service.IService;

namespace LunchPoll.MVC.Service
{
    public class EmployeeService(IBaseService baseService) : IEmployeeService
    {
        private const string Employees = "Employees";
        public async Task<ResponseDto> GetAllEmployeesAsync()
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Employees}"
            });
        }

        public async Task<ResponseDto> GetEmployeeByIdAsync(string id)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Employees}/{id}"
            });
        }

        public async Task<ResponseDto> CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Employees}",
                Data = employeeDto
            });
        }

        public async Task<ResponseDto> DeleteEmployeeAsync(string id)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.DELETE,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Employees}/{id}"
            });
        }

        public async Task<ResponseDto> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.PUT,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Employees}/{employeeDto.Id}",
                Data = employeeDto,
            });
        }
    }
}
