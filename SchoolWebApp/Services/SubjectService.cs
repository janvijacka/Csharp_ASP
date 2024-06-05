using Microsoft.EntityFrameworkCore;
using SchoolWebApp.DTO;
using SchoolWebApp.Models;

namespace SchoolWebApp.Services
{
    public class SubjectService
    {
        private ApplicationDbContext _dbContext;
        public SubjectService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<SubjectDTO> GetSubjects()
        {
            var allSubjects = _dbContext.Subjects;
            var subjectsDtos = new List<SubjectDTO>();
            foreach (var subject in allSubjects)
            {
                subjectsDtos.Add(ModelToDto(subject));
            }
            return subjectsDtos;
        }

        private static SubjectDTO ModelToDto(Subject subject)
        {
            return new SubjectDTO
            {
                Name = subject.Name,
                Id = subject.Id
            };
        }

        public async Task AddSubjectAsync(SubjectDTO subjectDto)
        {
            await _dbContext.Subjects.AddAsync(DtoToModel(subjectDto));
            await _dbContext.SaveChangesAsync();
        }

        private static Subject DtoToModel(SubjectDTO subjectDto)
        {
            return new Subject
            {
                Name = subjectDto.Name,
                Id = subjectDto.Id
            };
        }

        internal async Task<SubjectDTO> GetByIdAsync(int id)
        {
            var subject = await VerifyExistenceAsync(id);
            if (subject == null)
            {
                return null;
            }
            return ModelToDto(subject);
        }

        private async Task<Subject> VerifyExistenceAsync(int id)
        {
            var subject = await _dbContext.Subjects.FirstOrDefaultAsync(subject => subject.Id == id);
            if (subject == null)
            {
                return null;
            }
            return subject;
        }

        internal async Task UpdateAsync(int id, SubjectDTO subjectDTO)
        {
            _dbContext.Subjects.Update(DtoToModel(subjectDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id)
        {
            var subjectToDelete = await _dbContext.Subjects.FirstOrDefaultAsync(subject => subject.Id == id);
            //if (studentToDelete == null) {
            //    return null;
            //}
            _dbContext.Subjects.Remove(subjectToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
