using API_PIS4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_PIS4.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		readonly IConfiguration _config;
		public string? constr;

		UserLoginDB db = new UserLoginDB();
		public HomeController(IConfiguration config)
		{
			_config = config;
		}
	}
}
