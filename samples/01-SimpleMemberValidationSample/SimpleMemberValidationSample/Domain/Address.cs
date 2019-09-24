using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMemberValidationSample.Domain
{
	public class Address
	{
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string Line3 { get; set; }
		public string Town { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public Country Country { get; set; }
	}
}
