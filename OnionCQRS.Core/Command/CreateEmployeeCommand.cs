using MediatR;
using OnionCQRS.Core.DomainModels;

namespace OnionCQRS.Core.Command
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
