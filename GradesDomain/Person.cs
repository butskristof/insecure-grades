using System;

namespace GradesDomain
{
	public abstract class Person
	{
		protected Person()
		{
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}