using System;
using System.Collections.Generic;
using GradesDomain;

namespace GradesRepository
{
	public interface ITestRepository
	{
		List<Test> ReadTestsForStudent(Guid id);
		Test ReadTest(Guid id);
		void CreateTest(Test t);
	}
}