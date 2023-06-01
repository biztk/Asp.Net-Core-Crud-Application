namespace DayOneAssignment.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; } 
        public string? EmployeeFirstName { get; set; }
       public string ? EmployeeLastName { get; set; }
        public DateTime ? BirthDate { get; set; }

        public int CompanyId { get; set; }
        public Company ? Company { get; set; }


    }
}
