using System;

namespace SimpleMemberValidationSample.Domain
{
	public class Person
	{
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public DateTime? DateOfDeath { get; set; }
		public Address HomeAddress { get; set; }
	}
}
