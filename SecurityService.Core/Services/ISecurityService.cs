using SecurityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityService.Core.Services
{
	public interface ISecurityService
	{
		/// <summary>
		/// Saves the truck.
		/// </summary>
		/// <param name="truck">The truck.</param>
		/// <returns></returns>
		Task<Truck> SaveTruck(Truck truck);

		/// <summary>
		/// Checks the outstanding checks.
		/// </summary>
		Task CheckOutstandingChecks();

		/// <summary>
		/// Checks the truck.
		/// </summary>
		/// <param name="truck">The truck.</param>
		Task CheckTruck(Truck truck);
	}
}
