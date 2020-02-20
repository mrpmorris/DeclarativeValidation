namespace AspNetCoreMvc.Models
{
	public class Address
	{
		public string[] Lines { get; set; }
		public string Area { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public Country Country { get; set; }
	}
}
