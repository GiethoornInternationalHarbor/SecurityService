using System.ComponentModel.DataAnnotations;

namespace SecurityService.Core.Models
{
	public class Product
	{
		[Key]
		public string Name { get; set; }
		public string Type { get; set; }
	}
}