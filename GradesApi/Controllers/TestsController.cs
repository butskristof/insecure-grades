using System;
using GradesApi.Models.Data;
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

		[HttpPut("{id}")]
		public IActionResult UpdateTestScore(string id, [FromBody] TestData data)
		{
			var guid = Guid.Parse(id);
			var test = _repository.ReadTest(guid);
			test.Score = data.Score;
			return Ok(test);
		}
	}
}