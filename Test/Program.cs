using System;
using System.Linq;
using GradesDomain;
using GradesRepository;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			IPersonRepository personRepo = new PersonRepository();
			ITestRepository testRepo = new TestRepository();

			var people = personRepo.ReadPersons();
			var student = people.FirstOrDefault(p => p is Student) as Student;
			var t = new GradesDomain.Test()
			{
				Student = student,
				MaxScore = 10,
				Score = 5
			};
			
			testRepo.CreateTest(t);

			var count = testRepo.ReadTestsForStudent(student.Id).Count;
			Console.WriteLine(count);
		}
	}
}