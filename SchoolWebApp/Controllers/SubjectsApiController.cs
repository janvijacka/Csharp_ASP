using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApp.DTO;
using SchoolWebApp.Services;

namespace SchoolWebApp.Controllers {
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsApiController : ControllerBase {
        private SubjectService _subjectService;

        public SubjectsApiController(SubjectService subjectService) {
            _subjectService = subjectService;
        }

        ActionResult<IEnumerable<SubjectDTO>> Index() {
            var allSubjects = _subjectService.GetSubjects();
            return Ok(allSubjects);
        }
    }
}
