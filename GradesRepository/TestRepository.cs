using System;
using System.Collections.Generic;
using System.Linq;
using GradesDomain;

namespace GradesRepository
{
	public class TestRepository : ITestRepository
	{
		public List<Test> Tests { get; set; }

		public TestRepository()
		{
			this.Tests = new List<Test>();
		}

		public List<Test> ReadTestsForStudent(Guid id)
		{
			return this.Tests;
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