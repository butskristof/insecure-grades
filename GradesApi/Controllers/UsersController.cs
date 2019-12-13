using System;
using GradesApi.Models.Data;
using GradesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradesApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[Route("GetToken")]
		[HttpPost]
		public IActionResult GetToken([FromBody] LoginData data)
		{
			try
			{
				return Ok(_userService.GetToken(data.Id, data.Password));
			}
			catch (UnauthorizedAccessException e)
			{
				return Unauthorized();
			}
		}
	}
}