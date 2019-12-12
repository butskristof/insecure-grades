using System.Net.Mime;
using GradesApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradesApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class InitController : ControllerBase
	{
		private readonly AppSeeder _appSeeder;

		public InitController(AppSeeder appSeeder)
		{
			_appSeeder = appSeeder;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Init()
		{
			_appSeeder.Init();
			
			return Ok();
		}
	}
}