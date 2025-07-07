
using System.ComponentModel.DataAnnotations;

namespace LunchPoll.MVC.Models.Dtos
{
    public class EmployeeDto
    {
        public string? Id { get; set; }
        
        [Required]
        public  string? Name { get; set; }
        //public ICollection<Poll> Polls { get; set; }
    }
}
