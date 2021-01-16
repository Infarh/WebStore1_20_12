using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewModels
{
    public class EmployeesViewModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>Имя</summary>
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 15 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата имени")]
        public string Name { get; set; }

        /// <summary>Фамилия</summary>
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательной")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина фамилии должна быть от 3 до 15 символов")]
        public string LastName { get; set; }

        /// <summary>Отчество</summary>
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        /// <summary>Возраст</summary>
        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "Сотрудники должны быть в возрасте от 18 до 80 лет")]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            yield return ValidationResult.Success;
        }
    }
}
