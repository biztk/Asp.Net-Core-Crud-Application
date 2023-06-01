using System.Collections.Generic;

namespace DayOneAssignment.Models
{
    public class Company
    {

        //public Guid id { get; set; }
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; } 
        public string ? CompanyAddress { get; set; }
        public string ? CompanyDescription { get; set; }

        public List<Employee>? Employees { get; set; }
        //public  ICollection<Employee> ? employees { get; set; }   


        
}
}
