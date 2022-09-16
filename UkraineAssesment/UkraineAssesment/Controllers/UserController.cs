using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using UkraineAssesment.Models;

namespace UkraineAssesment.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{

		private readonly ILogger<UserController> _logger;
		private readonly ApplicationDBContext _dbContext;
		private readonly IDataProtectionProvider _protectionProvider;
		private readonly IConfiguration _configuration;
		public UserController(ILogger<UserController> logger, ApplicationDBContext applicationDBContext, IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
		{
			_logger = logger;
			_dbContext = applicationDBContext;
			_protectionProvider = dataProtectionProvider;
			_configuration = configuration;
		}

		[HttpGet]
		public IEnumerable<User> Get()
		{
			return _dbContext.Users.ToList(); ;
		}


		[HttpGet("GetSingle")]
		public User GetSingle(Guid Id)
		{
			
			var user = _dbContext.Users.Find(Id);			
			return user;
		}

		[HttpPost]
		public User Post([FromBody] User user)
		{
			
			var key = Guid.NewGuid().ToString();
			var protector = _protectionProvider.CreateProtector(key);
			user.Password = protector.Protect(user.Password);
			if (user.UserId == Guid.Empty)
			{
				user.UserId = Guid.NewGuid();
				_dbContext.Users.Add(user);
			}
			else
			{
				_dbContext.Users.Update(user);
			}
			_dbContext.SaveChanges();
			return user;
		}

		[HttpPost("Delete")]
		public StatusCodeResult Delete([FromBody] User user)
		{
			_dbContext.Users.Remove(user);
			_dbContext.SaveChanges();
			return Ok();
		}
	}
}