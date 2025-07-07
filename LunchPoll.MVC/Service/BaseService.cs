using LunchPoll.MVC.Helper;
using LunchPoll.MVC.Models;
using LunchPoll.MVC.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace LunchPoll.MVC.Service
{
    public class BaseService(IHttpClientFactory httpClientFactory) : IBaseService
    {
        public static string? ProductBaseApiUrl { get; set; }

        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("LunchPollAPI");

                var requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    requestMessage.Content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json"
                    );
                }

                requestMessage.Method = requestDto.ApiType switch
                {
                    ApiTypeEnum.GET => HttpMethod.Get,
                    ApiTypeEnum.POST => HttpMethod.Post,
                    ApiTypeEnum.PUT => HttpMethod.Put,
                    ApiTypeEnum.DELETE => HttpMethod.Delete,
                    _ => throw new NotImplementedException()
                };

                HttpResponseMessage? apiResponse = null;

                apiResponse = await httpClient.SendAsync(requestMessage);
                if (apiResponse.StatusCode == HttpStatusCode.OK)
                {
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                    return apiResponseDto;
                }

                return new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = apiResponse.ReasonPhrase,
                    StatusCode = apiResponse.StatusCode
                };
            }

            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
