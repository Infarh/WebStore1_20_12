using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")] // [controller] = EmployeesApi
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesApiController> _Logger;

        public EmployeesApiController(IEmployeesData EmployeesData, ILogger<EmployeesApiController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

        [HttpGet]
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")]
        public Employee Get(int id) => _EmployeesData.Get(id);

        [HttpPost]
        public int Add(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                _Logger.LogWarning("Ошибка модели данных при добавлении нового сотрудника {0} {1} {2}",
                    employee.LastName, employee.FirstName, employee.Patronymic);
                return 0;
            }

            _Logger.LogInformation("Добавление сотрудника {0} {1} {2}",
                employee.LastName, employee.FirstName, employee.Patronymic);

            var id = _EmployeesData.Add(employee);

            if (id > 0)
                _Logger.LogInformation("Cотрудник [id:{0}] {1} {2} {3} добавлен успешно",
                    employee.Id, employee.LastName, employee.FirstName, employee.Patronymic);
            else
                _Logger.LogWarning("Ошибка при добавлении сотрудника {0} {1} {2}",
                    employee.LastName, employee.FirstName, employee.Patronymic);

            return id;
        }

        [HttpPut/*("{id}")*/]
        public void Update(/*int id,*/ Employee employee) => _EmployeesData.Update(employee);

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            if(result)
                _Logger.LogInformation("Сотрудник с id:{0} успешно удалён", id);
            else
                _Logger.LogWarning("ошибка при попытке удаления сотрдуника с id:{0}", id);

            return result;
        }
    }
}
