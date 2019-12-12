using System;

namespace GradesDomain
{
	public abstract class Person
	{
		protected Person()
		{
			this.Id = new Guid();
		}

		public Guid Id { get; }
		public string Name { get; set; }
	}
}