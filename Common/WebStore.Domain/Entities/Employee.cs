using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    /// <summary>Сотрудник</summary>
    public class Employee : Entity
    {
        /// <summary>Имя</summary>
        public string FirstName { get; set; }

        /// <summary>Фамилия</summary>
        public string LastName { get; set; }

        /// <summary>Отчество</summary>
        public string Patronymic { get; set; }

        /// <summary>Возраст</summary>
        public int Age { get; set; }
    }
}
