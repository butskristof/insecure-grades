using System;
using System.Collections.Generic;
using System.Linq;
using GradesDomain;

namespace GradesRepository
{
	public class PersonRepository : IPersonRepository
	{
		public List<Person> Persons { get; set; }

		public PersonRepository()
		{
			this.Persons = new List<Person>()
			{
				new Student()
				{
					Name = "Joske Vermeulen"
				},
				new Teacher()
				{
					Name = "Pater Damiaan"
				}
			};
		}

		public List<Person> ReadPersons()
		{
			return this.Persons;
		}

		public Person ReadPerson(Guid id)
		{
			return this.Persons.SingleOrDefault(p => p.Id == id);
		}
	}
}