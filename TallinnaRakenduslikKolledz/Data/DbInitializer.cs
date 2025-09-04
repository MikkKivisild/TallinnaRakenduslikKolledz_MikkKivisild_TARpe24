using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolledz.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {
            context.Database.EnsureCreated();
            if(context.Students.Any())
            { 
                return; 
            }

            var students = new Student[]
            {
                new Student
                {
                    FirstName = "sdf",
                    LastName = "AAA",
                    EnrollmentDate = DateTime.Now,
                    Gender = "M",
                    Age = 21
                    EnrollmentStatus = "A"
                }
            };
        }
    }
}
