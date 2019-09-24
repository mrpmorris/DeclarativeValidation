namespace AspNetCoreMvc.Models
{
	public class Person
	{
		public string Salutation { get; set; }
		public string GivenName { get; set; }
		public string FamilyName { get; set; }
		public string EmailAddress { get; set; }
		public Address Address { get; set; }
		public Address[] OtherAddresses { get; set; }
	}
}
