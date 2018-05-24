using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityService.Core.Models
{
	public class Product
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type of the product.
		/// </summary>
		/// <value>
		/// The type of the product.
		/// </value>
		public ProductType ProductType { get; set; }
	}
}