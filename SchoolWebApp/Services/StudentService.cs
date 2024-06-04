using Microsoft.EntityFrameworkCore;
using SchoolWebApp.DTO;
using SchoolWebApp.Models;

namespace SchoolWebApp.Services {
    public class StudentService {
        private ApplicationDbContext _dbContext;

        public StudentService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<StudentDTO> GetStudents() {
            var allStudents = _dbContext.Students;
            var studentsDtos = new List<StudentDTO>();
            foreach (var student in allStudents) {
                studentsDtos.Add(ModelToDto(student));
            }
            return studentsDtos;
        }

        private static StudentDTO ModelToDto(Student student) {
            return new StudentDTO {
                DateOfBirth = student.DateOfBirth,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Id = student.Id
            };
        }

        public async Task AddStudentAsync(StudentDTO studentDto) {
            await _dbContext.Students.AddAsync(DtoToModel(studentDto));
            await _dbContext.SaveChangesAsync();
        }

        private static Student DtoToModel(StudentDTO studentDto) {
            return new Student {
                DateOfBirth = studentDto.DateOfBirth,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Id = studentDto.Id
            };
        }

        internal async Task<StudentDTO> GetByIdAsync(int id) {
            var student = await VerifyExistenceAsync(id);
            if (student == null) {
                return null;
            }
            return ModelToDto(student);
        }

        private async Task<Student> VerifyExistenceAsync(int id) {
            var student = await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
            if (student == null) {
                return null;
            }
            return student;
        }

        internal async Task UpdateAsync(int id, StudentDTO studentDTO) {
            _dbContext.Students.Update(DtoToModel(studentDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var studentToDelete = await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
            //if (studentToDelete == null) {
            //    return null;
            //}
            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
