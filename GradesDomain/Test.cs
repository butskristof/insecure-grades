using System;

namespace GradesDomain
{
	public class Test
	{
		public Test()
		{
			this.Id = new Guid();
		}

		public Guid Id { get; }
		
		public double MaxScore { get; set; }
		public double Score { get; set; }
		
		public Student Student { get; set; }
	}
}