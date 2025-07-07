using LunchPoll.MVC.Models;

namespace LunchPoll.MVC.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
