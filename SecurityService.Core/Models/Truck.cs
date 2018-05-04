using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecurityService.Core.Models
{
	public class Truck
	{
		[Key]
		public string LicensePlate { get; set; }
		public Container Container { get; set; }
		public TruckStatus Status { get; set; }
		public SecurityStatus SecurityStatus { get; set; }
	}
}
