using System.ComponentModel.DataAnnotations;

namespace EmployeeProject1.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}