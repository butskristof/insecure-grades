using System;
using System.Collections.Generic;
using GradesDomain;

namespace DomainTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Student s = new Student()
			{
				Name = "Jan"
			};
			Teacher t = new Teacher()
			{
				Name = "Peter"
			};
			
			List<Person> list = new List<Person>()
			{
				s, t
			};

			list.ForEach(p =>
			{
				Console.WriteLine(p.Name);
				Console.WriteLine($"Student: {p is Student}");
				Console.WriteLine($"Teacher: {p is Teacher}");
			});
		}
	}
}