using System.Collections.Generic;

namespace OnionCQRS.Core.DomainModels
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeLimit { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}