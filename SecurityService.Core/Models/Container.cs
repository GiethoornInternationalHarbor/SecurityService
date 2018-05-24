using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecurityService.Core.Models
{
	public class Container
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the serial shipping container code.
		/// </summary>
		public string SerialShippingContainerCode { get; set; }

		/// <summary>
		/// Gets or sets the products.
		/// </summary>
		public List<Product> Products { get; set; }

		/// <summary>
		/// Gets or sets the type of the container.
		/// </summary>
		public ContainerType ContainerType { get; set; }
	}
}
