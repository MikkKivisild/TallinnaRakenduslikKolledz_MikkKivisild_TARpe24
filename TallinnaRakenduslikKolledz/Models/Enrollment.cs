using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledz.Models
{
    public enum Grade
    {
        A, B, C, D, F, X, MA
    }
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? CurrentGrade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
