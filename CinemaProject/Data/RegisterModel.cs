using System.ComponentModel.DataAnnotations;

namespace CinemaProject.Data
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage = "Неверный ввод")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
