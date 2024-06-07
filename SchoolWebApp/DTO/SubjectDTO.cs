using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolWebApp.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        [MaxLength(25)] //Omezí max délku name subjectu ale musí být do příslušného view zahrnut script a tag asp-validation-for
        [MinLength(4, ErrorMessage = "The subject name is too short, the minimum length is 4")]
        [DisplayName("Subject name")]
        public string Name { get; set; }
    }
}
