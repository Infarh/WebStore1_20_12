using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using WebStore.Clients.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger<EmployeesClient> _Logger;

        public EmployeesClient(IConfiguration Configuration, ILogger<EmployeesClient> Logger)
            : base(Configuration, "api/employees") =>
            _Logger = Logger;


        public IEnumerable<Employee> Get()
        {
            throw new NotImplementedException();
        }

        public Employee Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
