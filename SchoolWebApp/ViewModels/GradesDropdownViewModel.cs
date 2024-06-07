using SchoolWebApp.Models;

namespace SchoolWebApp.ViewModels {
    public class GradesDropdownViewModel {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public GradesDropdownViewModel() {
            Students = new List<Student>();
            Subjects = new List<Subject>();
        }
    }
}
