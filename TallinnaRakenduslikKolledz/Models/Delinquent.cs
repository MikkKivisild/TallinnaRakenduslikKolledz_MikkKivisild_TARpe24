using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledz.Models
{
	public class Delinquent
	{
		public enum eViolation
		{
			none, slight, significant, crutial, Big
		}
		public enum eViolator
		{
			Student, Instructor
		}
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public eViolation Violation { get; set; }
		public eViolator Violator { get; set; }
		public string Description { get; set; }
	}
}
