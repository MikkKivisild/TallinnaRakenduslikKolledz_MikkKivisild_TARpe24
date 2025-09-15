using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledz.Models
{
	public class Instructor
	{
		[Key]
		public int ID { get; set; }
		/**/
		[Required]
		[StringLength(50)]
		[Display(Name = "Perekonnanimi")]
		public string LastName { get; set; }
		/**/
		[Required]
		[StringLength(50)]
		[Display(Name = "Eesnimi")]
		public string FirstName { get; set; }
		/**/
		[Display(Name = "Õpetaja nimi")]
		public string FullName
		{
			get	{ return LastName + ", " + FirstName; }
		}
		/**/
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		[Display(Name = "Tööleasumiskuupäev")]
		public DateTime HireDate { get; set; }

		public ICollection<CourseAssignment>? CourseAssignments { get; set; }
		public OfficeAssignment? OfficeAssignment { get; set; }

		/* lisa kolm omadust õpetajale, ise mõtled välja */

		[Display(Name = "Email")]
		public string Email { get; set; }
		[Display(Name = "Telefoninumber")]
		public int PhoneNumber { get; set; }
		[Display(Name = "Aadress")]
		public string Address { get; set; }
	}
}
