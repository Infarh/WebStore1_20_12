using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Users")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _Employees;

        public EmployeesController(IEmployeesData Employees) => _Employees = Employees;

        //[Route("All")]
        public IActionResult Index()
        {
            var employees = _Employees.Get();
            return View(employees);
        }

        //[Route("Info(id-{id})")]
        public IActionResult Details(int id)
        {
            var employee = _Employees.Get(id);
            if (employee is not null)
                return View(employee);

            return NotFound();
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Create() => View("Edit", new EmployeesViewModel());

        #region Edit

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeesViewModel());

            if (id < 0)
                return BadRequest();

            var employee = _Employees.Get((int)id);
            if (employee is null)
                return NotFound();

            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                MiddleName = employee.Patronymic,
                Age = employee.Age,
            });
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeesViewModel Model)
        {
            if (Model.Age == 25)
                ModelState.AddModelError("Age", "Возраст не должен быть равен 25");

            if (Model.LastName == "Иванов" && Model.Age == 30)
                ModelState.AddModelError("", "Странный человек...");

            if (!ModelState.IsValid) return View(Model);

            if (Model is null)
                throw new ArgumentNullException(nameof(Model));

            var employee = new Employee
            {
                Id = Model.Id,
                LastName = Model.LastName,
                FirstName = Model.Name,
                Patronymic = Model.MiddleName,
                Age = Model.Age,
            };

            if (employee.Id == 0)
                _Employees.Add(employee);
            else
                _Employees.Update(employee);

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var employee = _Employees.Get(id);
            if (employee is null)
                return NotFound();

            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                MiddleName = employee.Patronymic,
                Age = employee.Age,
            });
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteConfirmed(int id)
        {
            _Employees.Delete(id);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
