using LunchPoll.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LunchPoll.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController(ApplicationDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            return dbContext.Restaurants.Any()
                ? Ok(await dbContext.Restaurants.ToListAsync())
                : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(string id)
        {
            var restaurant = await dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
            return restaurant != null ? Ok(restaurant) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] string restaurantName)
        {
            if (string.IsNullOrWhiteSpace(restaurantName))
            {
                return BadRequest("Restaurant name cannot be empty.");
            }
            dbContext.Restaurants.Add(new Restaurant { Name = restaurantName });
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRestaurants), new { name = restaurantName }, restaurantName);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(string id, [FromBody] string restaurantName)
        {
            if (string.IsNullOrWhiteSpace(restaurantName))
            {
                return BadRequest("Restaurant name cannot be empty.");
            }
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            restaurant.Name = restaurantName;
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(string id)
        {
            var restaurant = dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            dbContext.Restaurants.Remove(restaurant);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
