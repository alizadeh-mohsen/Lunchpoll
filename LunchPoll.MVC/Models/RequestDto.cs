using LunchPoll.MVC.Helper;

namespace LunchPoll.MVC.Models
{
    public class RequestDto
    {
        public ApiTypeEnum ApiType { get; set; }
        public string? Url { get; set; }
        public object? Data { get; set; }
    }

   
}
