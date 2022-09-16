namespace UkraineAssesment.Models
{
	public class User
	{
		public Guid ? UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
		public string ? MobileCode { get; set; }
		public string ? Mobile { get; set; }
		public int UserRole { get; set; }
		public int ? Client { get; set; }
		public int ? Status { get; set; }

	}
}
