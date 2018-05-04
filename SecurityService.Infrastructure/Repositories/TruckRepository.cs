using Microsoft.EntityFrameworkCore;
using SecurityService.Core;
using SecurityService.Core.Models;
using SecurityService.Core.Repositories;
using SecurityService.Infrastructure.Database;
using System;
using System.Collections.Generic;
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

		public async Task<Truck> Save(Truck value)
		{
			var exists = await _database.Trucks.AnyAsync(r => r.LicensePlate == value.LicensePlate);

			Truck savedTruck;
			if (exists)
			{
				var existingTruck = await _database.Trucks.LastOrDefaultAsync(r => r.LicensePlate == value.LicensePlate);

				existingTruck.Container = value.Container;
				existingTruck.Status = value.Status;

				savedTruck = existingTruck;
			}
			else
			{
				savedTruck = (await _database.Trucks.AddAsync(value)).Entity;
			}

			await _database.SaveChangesAsync();

			return savedTruck;
		}

		public async Task Delete(string plate)
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
	}
}
