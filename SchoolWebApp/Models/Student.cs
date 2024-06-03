using System.Diagnostics.CodeAnalysis;

namespace SchoolWebApp.Models {
    public class Student {
        public int Id { get; set; }
        [NotNull]
        public string? FirstName { get; set; } //string? aby vlastnost mohla být null, [NotNull] zase zabranuje aby bylo null a vyžaduje vyplnění, ale bez warningu
        [NotNull]
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
