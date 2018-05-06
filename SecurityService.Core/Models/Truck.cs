using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace SecurityService.Core.Models
{
	public class Truck
	{
		[Key]
		public string LicensePlate { get; set; }
		public Container Container { get; set; }

		[IgnoreDataMember]
		public SecurityStatus SecurityStatus { get; set; }
	}
}
