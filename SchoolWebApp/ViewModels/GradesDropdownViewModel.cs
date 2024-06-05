using SchoolWebApp.Models;

namespace SchoolWebApp.ViewModels {
    public class GradesDropdownViewModel {
        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }
        public GradesDropdownViewModel() {
            Students = new List<Student>();
            Subjects = new List<Subject>();
        }
    }
}
