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
    public class RestaurantsController(ApplicationDbContext _context, IMapper _autoMapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> Get()
        {
            try
            {
                var Restaurants = await _context.Restaurants.ToListAsync();

                if (Restaurants == null || !Restaurants.Any())
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "No Restaurants found."
                    };
                }

                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<IEnumerable<RestaurantDto>>(Restaurants),
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
                var Restaurant = await _context.Restaurants.FindAsync(id);
                if (Restaurant == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Restaurant not found."
                    });
                }
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<RestaurantDto>(Restaurant),
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
        public async Task<ActionResult<ResponseDto>> Create([FromBody] RestaurantDto RestaurantDto)
        {
            try
            {
                if (RestaurantDto == null)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Restaurant data."
                    });
                }
                var Restaurant = _autoMapper.Map<Restaurant>(RestaurantDto);
                await _context.Restaurants.AddAsync(Restaurant);
                await _context.SaveChangesAsync();
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<RestaurantDto>(Restaurant),
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
        public async Task<ActionResult<ResponseDto>> Update(string id, [FromBody] RestaurantDto RestaurantDto)
        {
            try
            {
                if (RestaurantDto == null || id != RestaurantDto.Id)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Restaurant data."
                    });
                }
                var existingRestaurant = await _context.Restaurants.FindAsync(id);
                if (existingRestaurant == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Restaurant not found."
                    });
                }

                var updatedRestaurant = _autoMapper.Map(RestaurantDto, existingRestaurant);
                _context.Restaurants.Update(updatedRestaurant);
                await _context.SaveChangesAsync();
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<RestaurantDto>(existingRestaurant),
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
                var Restaurant = await _context.Restaurants.FindAsync(id);
                if (Restaurant == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        ErrorMessage = "Restaurant not found."
                    });
                }
                _context.Restaurants.Remove(Restaurant);
                await _context.SaveChangesAsync();
                return Ok(new ResponseDto
                {
                    Result = "Restaurant deleted successfully."
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
