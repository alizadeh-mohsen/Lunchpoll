using LunchPoll.Api.Data;

namespace LunchPoll.MVC.Models
{
    public class PollDto
    {
        public required string EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public required string RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public DateTime Date { get; set; } 
        public bool Selected { get; set; }
    }
}
