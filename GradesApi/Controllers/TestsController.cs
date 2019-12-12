using System;
using GradesRepository;
using Microsoft.AspNetCore.Mvc;

namespace GradesApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TestsController : Controller
	{
		private readonly ITestRepository _repository;

		public TestsController(ITestRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("{id}")]
		public IActionResult GetTestForStudent(string id)
		{
			var guid = Guid.Parse(id);
			return Ok(_repository.ReadTestsForStudent(guid));
		}
	}
}