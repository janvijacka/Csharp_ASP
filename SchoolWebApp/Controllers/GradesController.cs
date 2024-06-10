using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebApp.DTO;
using SchoolWebApp.Services;

namespace SchoolWebApp.Controllers {
    [Authorize(Roles = "Admin")]
    public class GradesController : Controller {
        private GradeService _gradeService;

        public GradesController(GradeService gradeService) {
            _gradeService = gradeService;
        }


        public async Task<IActionResult> CreateAsync() {
            await FillSelectsAsync();
            return View();
        }

        private async Task FillSelectsAsync() {
            var gradesDropdownData = await _gradeService.GetGradesDropdownsData();
            ViewBag.Students = new SelectList(gradesDropdownData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropdownData.Subjects, "Id", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GradeDTO gradeDTO) {
            await _gradeService.CreateAsync(gradeDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index() {
            var allGrades = await _gradeService.GetAllVMsAsync();
            return View(allGrades);
        }

        public async Task<IActionResult> Update(int id) {
            var gradeToEdit = await _gradeService.GetByIdAsync(id);
            if (gradeToEdit == null) {
                return View("NotFound");
            }
            var gradeDTO = _gradeService.ModelToDto(gradeToEdit);
            await FillSelectsAsync();
            return View(gradeDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Update(GradeDTO gradeDTO, int id) {
            await _gradeService.UpdateAsync(gradeDTO, id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _gradeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
