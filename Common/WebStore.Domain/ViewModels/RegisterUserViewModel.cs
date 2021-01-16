using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
        
    }
}
