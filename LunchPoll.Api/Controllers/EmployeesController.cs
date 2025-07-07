using AutoMapper;
using LunchPoll.Api.Data;
using LunchPoll.Api.Data.Dtos;
using LunchPoll.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LunchPoll.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(ApplicationDbContext _context, IMapper _autoMapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> Get()
        {
            try
            {
                var Employees = await _context.Employees.ToListAsync();

                if (Employees == null || !Employees.Any())
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "No Employees found."
                    };
                }

                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<IEnumerable<EmployeeDto>>(Employees),
                };
                return Ok(responseDto);

            }
            catch (Exception ex)
            {

                return new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> Get(string id)
        {
            try
            {
                var Employee = await _context.Employees.FindAsync(id);
                if (Employee == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Employee not found."
                    });
                }
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<EmployeeDto>(Employee),
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] EmployeeDto EmployeeDto)
        {
            try
            {
                if (EmployeeDto == null)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Employee data."
                    });
                }
                var Employee = _autoMapper.Map<Employee>(EmployeeDto);
                await _context.Employees.AddAsync(Employee);
                await _context.SaveChangesAsync();
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<EmployeeDto>(Employee),
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> Update(string id, [FromBody] EmployeeDto EmployeeDto)
        {
            try
            {
                if (EmployeeDto == null || id != EmployeeDto.Id)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Employee data."
                    });
                }
                var existingEmployee = await _context.Employees.FindAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Employee not found."
                    });
                }

                var updatedEmployee = _autoMapper.Map(EmployeeDto, existingEmployee);
                _context.Employees.Update(updatedEmployee);
                await _context.SaveChangesAsync();
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<EmployeeDto>(existingEmployee),
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> Delete(string id)
        {
            try
            {
                var Employee = await _context.Employees.FindAsync(id);
                if (Employee == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Employee not found."
                    });
                }
                _context.Employees.Remove(Employee);
                await _context.SaveChangesAsync();
                return Ok(new ResponseDto
                {
                    Result = "Employee deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                });
            }
        }

    }
}
