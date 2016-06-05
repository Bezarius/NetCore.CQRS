using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnionCQRS.Core.DomainModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int DepartamentId { get; set; }
        public virtual Departament Departament { get; set; }
    }
}
