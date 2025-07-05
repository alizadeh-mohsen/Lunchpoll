namespace LunchPoll.Api.Data
{
    public class Restaurant
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public ICollection<Poll> Polls { get; set; } = [];
    }
}
