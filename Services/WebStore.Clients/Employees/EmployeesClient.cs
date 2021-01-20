using System.Collections.Generic;
using System.Net.Http;
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


        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public int Add(Employee employee) => Post(Address, employee).Content.ReadAsAsync<int>().Result;

        public void Update(Employee employee)
        {
            _Logger.LogInformation("Редактирование сотрудника с id:{0}", employee.Id);
            Put(Address, employee);
        }

        public bool Delete(int id) => Delete($"{Address}/{id}").IsSuccessStatusCode;
    }
}
