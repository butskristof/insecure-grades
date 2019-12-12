using System;
using System.Collections.Generic;
using GradesDomain;

namespace GradesRepository
{
	public interface IPersonRepository
	{
		List<Person> ReadPersons();
		Person ReadPerson(Guid id);
	}
}