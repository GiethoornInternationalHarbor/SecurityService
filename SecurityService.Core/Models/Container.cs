using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecurityService.Core.Models
{
	public class Container
	{
		[Key]
		public string Number { get; set; }
		public Product Product { get; set; }
	}
}
