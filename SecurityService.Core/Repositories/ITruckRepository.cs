using SecurityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityService.Core.Repositories
{
	public interface ITruckRepository
	{
		/// <summary>
		/// Creates or updates the specified object.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		Task<Truck> Save(Truck value);
		
		/// <summary>
		/// Deletes the specified external identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task Delete(string plate);
	}
}
