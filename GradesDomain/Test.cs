using System;

namespace GradesDomain
{
	public class Test
	{
		public Test()
		{
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }
		
		public double MaxScore { get; set; }
		public double Score { get; set; }
		
		public Student Student { get; set; }
	}
}