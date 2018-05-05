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
		/// Creates the truck asynchronous.
		/// </summary>
		/// <param name="truck">The truck.</param>
		/// <returns></returns>
		Task<Truck> CreateTruckAsync(Truck truck);

		/// <summary>
		/// Updates the truck container asynchronous.
		/// </summary>
		/// <param name="plate">The plate.</param>
		/// <param name="container">The container.</param>
		/// <returns></returns>
		Task UpdateTruckContainerAsync(string plate, Container container = null);

		/// <summary>
		/// Gets the truck asynchronous.
		/// </summary>
		/// <param name="plate">The plate.</param>
		/// <returns></returns>
		Task<Truck> GetTruckAsync(string plate);

		/// <summary>
		/// Saves the truck asynchronous.
		/// </summary>
		/// <param name="truck">The truck.</param>
		/// <returns></returns>
		Task<Truck> SaveTruckAsync(Truck truck);

		/// <summary>
		/// Checks the outstanding checks asynchronous.
		/// </summary>
		/// <returns></returns>
		Task CheckOutstandingChecksAsync();

		/// <summary>
		/// Checks the truck asynchronous.
		/// </summary>
		/// <param name="truck">The truck.</param>
		/// <returns></returns>
		Task CheckTruckAsync(Truck truck);
	}
}
