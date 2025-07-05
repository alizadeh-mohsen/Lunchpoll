namespace LunchPoll.Api.Data
{
    public class Poll
    {
        public required string EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public required string RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool Selected { get; set; }
    }
}
