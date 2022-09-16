using Microsoft.EntityFrameworkCore;
using UkraineAssesment.Models;

namespace UkraineAssesment
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
		}
		//Create the DataSet for the Employee         
		public DbSet<User> Users { get; set; }

		
	}
}
