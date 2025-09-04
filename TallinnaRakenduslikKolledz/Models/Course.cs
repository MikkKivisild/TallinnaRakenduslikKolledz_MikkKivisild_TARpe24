using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledz.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set;  }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
