using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entity
{
    public class Department
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}