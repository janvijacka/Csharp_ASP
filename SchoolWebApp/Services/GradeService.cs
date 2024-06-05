using Microsoft.EntityFrameworkCore;
using SchoolWebApp.DTO;
using SchoolWebApp.Models;
using SchoolWebApp.ViewModels;

namespace SchoolWebApp.Services {
    public class GradeService {
        private ApplicationDbContext _context;
        public GradeService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<GradesDropdownViewModel> GetGradesDropdownsData() {
            var gradesDropdownsData = new GradesDropdownViewModel() {
                Students = await _context.Students.OrderBy(student => student.LastName).ToListAsync(),
                Subjects = await _context.Subjects.OrderBy(subject => subject.Name).ToListAsync()
            };
            return gradesDropdownsData;
        }

        internal async Task CreateAsync(GradeDTO gradeDTO) {
            Grade gradeToInsert = await DtoToModel(gradeDTO);
            await _context.AddAsync(gradeToInsert);
            await _context.SaveChangesAsync();
        }

        private async Task<Grade> DtoToModel(GradeDTO gradeDTO) {
            return new Grade() {
                Date = DateTime.Today,
                Mark = gradeDTO.Mark,
                Topic = gradeDTO.Topic,
                Student = await _context.Students.FirstOrDefaultAsync(student => student.Id == gradeDTO.StudentId),
                Subject = await _context.Subjects.FirstOrDefaultAsync(subject => subject.Id == gradeDTO.SubjectId),
                Id = gradeDTO.Id
            };
        }

        public async Task<IEnumerable<GradesViewModel>> GetAllVMsAsync() {
            List<Grade> grades = await _context.Grades.Include(gr => gr.Student).Include(gr => gr.Subject).ToListAsync();
            List<GradesViewModel> gradesViewModels = new List<GradesViewModel>();
            foreach (Grade grade in grades) {
                gradesViewModels.Add(new GradesViewModel {
                    Date = grade.Date,
                    Mark = grade.Mark,
                    Topic = grade.Topic,
                    Id = grade.Id,
                    StudentName = $"{grade.Student.LastName} {grade.Student.FirstName}",
                    SubjectName = grade.Subject.Name
                });
            }
            return gradesViewModels;
        }

        internal async Task<Grade> GetByIdAsync(int id) {
            return await _context.Grades.Include(gr => gr.Student).Include(gr => gr.Subject).FirstOrDefaultAsync(grade => grade.Id == id);
        }

        internal GradeDTO ModelToDto(Grade grade) {
            return new GradeDTO {
                Id = grade.Id,
                Date = grade.Date,
                Mark = grade.Mark,
                Topic = grade.Topic,
                StudentId = grade.Student.Id,
                SubjectId = grade.Subject.Id,
            };
        }

        internal async Task UpdateAsync(GradeDTO gradeDTO, int id) {
            //Grade updatedGrade =  await DtoToModel(gradeDTO);
            //_context.Grades.Update(updatedGrade);
            _context.Grades.Update(await DtoToModel(gradeDTO));
            await _context.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var gradeToDelete = await _context.Grades.FirstOrDefaultAsync(grade => grade.Id == id);
            _context.Grades.Remove(gradeToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
