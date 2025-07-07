using System.Net;

namespace LunchPoll.MVC.Models
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }=true;
        public string? ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }=HttpStatusCode.OK;
    }
}
