using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> Get();

        Employee Get(int id);

        int Add(Employee employee);

        void Update(Employee employee);

        bool Delete(int id);
    }
}
