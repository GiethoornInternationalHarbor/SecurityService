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
		/// Creates the truck asynchronous.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		Task<Truck> CreateAsync(Truck value);

		/// <summary>
		/// Updates the truck asynchronous.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		Task<Truck> UpdateAsync(Truck value);

		/// <summary>
		/// Deletes the truck asynchronous.
		/// </summary>
		/// <param name="plate">The plate.</param>
		/// <returns></returns>
		Task DeleteAsync(string plate);

		/// <summary>
		/// Gets the truck asynchronous.
		/// </summary>
		/// <param name="plate">The plate.</param>
		/// <returns></returns>
		Task<Truck> GetAsync(string plate);

		/// <summary>
		/// Updates the security status asynchronous.
		/// </summary>
		/// <param name="plate">The plate.</param>
		/// <param name="securityStatus">The security status.</param>
		/// <returns></returns>
		Task UpdateSecurityStatusAsync(string plate, SecurityStatus securityStatus);

		/// <summary>
		/// Gets the trucks needed to be checked asynchronous.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Truck>> GetTrucksNeededToBeCheckedAsync();

		/// <summary>
		/// Updates the container asynchronous.
		/// </summary>
		/// <param name="plate">The plate.</param>
		/// <param name="container">The container.</param>
		/// <returns></returns>
		Task UpdateContainerAsync(string plate, Container container = null);
	}
}
