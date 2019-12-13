using GradesRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradesApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PersonsController : Controller
	{
		private readonly IPersonRepository _repository;

		public PersonsController(IPersonRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public IActionResult GetPersons()
		{
			return Ok(_repository.ReadPersons());
		}
	}
}