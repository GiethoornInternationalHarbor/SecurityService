using Microsoft.EntityFrameworkCore;
using SecurityService.Core;
using SecurityService.Core.Models;
using SecurityService.Core.Repositories;
using SecurityService.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityService.Infrastructure.Repositories
{
	public class TruckRepository : ITruckRepository
	{
		private readonly SecurityDbContext _database;
		public TruckRepository(SecurityDbContext database)
		{
			_database = database;
		}

		public async Task<Truck> UpdateAsync(Truck value)
		{
			var exists = await _database.Trucks.AnyAsync(r => r.LicensePlate == value.LicensePlate);

			if (!exists)
			{
				throw new KeyNotFoundException($"Truck with license plate: {value.LicensePlate} not found.");
			}

			Truck existingTruck = await _database.Trucks.LastOrDefaultAsync(r => r.LicensePlate == value.LicensePlate);

			existingTruck.Container = value.Container;
			existingTruck.Status = value.Status;
			existingTruck.SecurityStatus = value.SecurityStatus;

			await _database.SaveChangesAsync();

			return existingTruck;
		}

		public async Task DeleteAsync(string plate)
		{
			Truck truckToDelete = new Truck() { LicensePlate = plate };
			_database.Entry(truckToDelete).State = EntityState.Deleted;
			await _database.SaveChangesAsync();
		}

		public async Task Update(Truck value)
		{
			_database.Trucks.Update(value);
			await _database.SaveChangesAsync();
		}

		public async Task UpdateSecurityStatusAsync(string plate, SecurityStatus securityStatus)
		{
			Truck existingTruck = await _database.Trucks.LastOrDefaultAsync(r => r.LicensePlate == plate);

			existingTruck.SecurityStatus = securityStatus;

			await _database.SaveChangesAsync();
		}

		public async Task<IEnumerable<Truck>> GetTrucksNeededToBeCheckedAsync()
		{
			return await _database.Trucks.Where(t => t.SecurityStatus != SecurityStatus.Completed).ToArrayAsync();
		}

		public async Task<Truck> CreateAsync(Truck value)
		{
			var newTruck = (await _database.Trucks.AddAsync(value)).Entity;
			await _database.SaveChangesAsync();

			return newTruck;
		}
	}
}
