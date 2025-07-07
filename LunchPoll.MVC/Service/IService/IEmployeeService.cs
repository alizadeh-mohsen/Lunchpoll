using LunchPoll.MVC.Models;
using LunchPoll.MVC.Models.Dtos;

namespace LunchPoll.MVC.Service.IService
{
    public interface IEmployeeService
    {
        Task<ResponseDto> GetAllEmployeesAsync();
        Task<ResponseDto> GetEmployeeByIdAsync(string id);
        Task<ResponseDto> CreateEmployeeAsync(EmployeeDto requestDto);
        Task<ResponseDto> UpdateEmployeeAsync(EmployeeDto requestDto);
        Task<ResponseDto> DeleteEmployeeAsync(string id);
    }
}
