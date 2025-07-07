using LunchPoll.MVC.Models;
using LunchPoll.MVC.Models.Dtos;

namespace LunchPoll.MVC.Service.IService
{
    public interface IRestaurantService
    {
        Task<ResponseDto> GetAllRestaurantsAsync();
        Task<ResponseDto> GetRestaurantByIdAsync(string id);
        Task<ResponseDto> CreateRestaurantAsync(RestaurantDto requestDto);
        Task<ResponseDto> UpdateRestaurantAsync(RestaurantDto requestDto);
        Task<ResponseDto> DeleteRestaurantAsync(string id);
    }
}
