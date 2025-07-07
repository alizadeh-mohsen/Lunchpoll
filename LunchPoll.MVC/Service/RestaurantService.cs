using LunchPoll.MVC.Helper;
using LunchPoll.MVC.Models;
using LunchPoll.MVC.Models.Dtos;
using LunchPoll.MVC.Service.IService;

namespace LunchPoll.MVC.Service
{
    public class RestaurantService(IBaseService baseService) : IRestaurantService
    {
        private const string Restaurants = "Restaurants";
        public async Task<ResponseDto> GetAllRestaurantsAsync()
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Restaurants}"
            });
        }

        public async Task<ResponseDto> GetRestaurantByIdAsync(string id)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Restaurants}/{id}"
            });
        }

        public async Task<ResponseDto> CreateRestaurantAsync(RestaurantDto RestaurantDto)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Restaurants}",
                Data = RestaurantDto
            });
        }

        public async Task<ResponseDto> DeleteRestaurantAsync(string id)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.DELETE,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Restaurants}/{id}"
            });
        }

        public async Task<ResponseDto> UpdateRestaurantAsync(RestaurantDto RestaurantDto)
        {
            return await baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.PUT,
                Url = $"{BaseUrl.ApiBaseApiUrl}/{Restaurants}/{RestaurantDto.Id}",
                Data = RestaurantDto,
            });
        }
    }
}


