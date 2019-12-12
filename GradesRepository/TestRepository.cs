using System;
using System.Collections.Generic;
using System.Linq;
using GradesDomain;

namespace GradesRepository
{
	public class TestRepository : ITestRepository
	{
		private List<Test> Tests { get; set; }

		public TestRepository()
		{
			this.Tests = new List<Test>();
		}

		public List<Test> ReadTestsForStudent(Guid id)
		{
			return this.Tests.Where(t => t.Student.Id == id).ToList();
		}

		public Test ReadTest(Guid id)
		{
			return this.Tests.SingleOrDefault(t => t.Id == id);
		}

		public void CreateTest(Test t)
		{
			this.Tests.Add(t);
		}
	}
}