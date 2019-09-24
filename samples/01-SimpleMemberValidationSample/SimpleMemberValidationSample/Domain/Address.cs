using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMemberValidationSample.Domain
{
	public class Address
	{
		public string[] Lines { get; set; }
		public string Town { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public Country Country { get; set; }
	}
}
