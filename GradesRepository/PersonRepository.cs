using System;
using System.Collections.Generic;
using System.Linq;
using GradesDomain;

namespace GradesRepository
{
	public class PersonRepository : IPersonRepository
	{
		private List<Person> Persons { get; set; }

		public PersonRepository()
		{
			this.Persons = new List<Person>()
			{
				new Student
				{
					Id = Guid.Parse("cea4e519-8411-4606-9908-8c0bf28525a0"),
					Name = "Joske Vermeulen"
				},
				new Teacher
				{
					Id = Guid.Parse("aead81b0-bf38-41ad-829e-8433f17c9f9e"),
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