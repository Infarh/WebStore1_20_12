using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>API управления сотрудниками</summary>
    [Route(WebAPI.Employees)]
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

        /// <summary>Получение всех сотрудников</summary>
        /// <returns>Список сотрудников</returns>
        [HttpGet]
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        /// <summary>Получение сотрудника по идентификатору</summary>
        /// <param name="id">Идентификатор сотрудника</param>
        [HttpGet("{id}")]
        public Employee Get(int id) => _EmployeesData.Get(id);

        /// <summary>Добавление нового сотрудника</summary>
        /// <param name="employee">Добавляемый сотрудник</param>
        /// <returns>Идентификатор нового сотрудника</returns>
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
                _Logger.LogInformation("Сотрудник [id:{0}] {1} {2} {3} добавлен успешно",
                    employee.Id, employee.LastName, employee.FirstName, employee.Patronymic);
            else
                _Logger.LogWarning("Ошибка при добавлении сотрудника {0} {1} {2}",
                    employee.LastName, employee.FirstName, employee.Patronymic);

            return id;
        }

        /// <summary>Редактирование сотрудника</summary>
        /// <param name="employee">Информация для изменения данных сотрудника</param>
        [HttpPut/*("{id}")*/]
        public void Update(/*int id,*/ Employee employee) => _EmployeesData.Update(employee);

        /// <summary>Удаление сотрудника по его id</summary>
        /// <param name="id">Идентификатор удаляемого сотрудника</param>
        /// <returns>Истина, если сотрудник был удалён</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            if(result)
                _Logger.LogInformation("Сотрудник с id:{0} успешно удалён", id);
            else
                _Logger.LogWarning("ошибка при попытке удаления сотрудника с id:{0}", id);

            return result;
        }
    }
}
