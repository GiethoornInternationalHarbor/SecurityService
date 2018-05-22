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
		private readonly SecurityDbContextFactory _securityDbContextFactory;
		public TruckRepository(SecurityDbContextFactory dbContextFactory)
		{
			_securityDbContextFactory = dbContextFactory;
		}

		public async Task<Truck> UpdateAsync(Truck value)
		{
			SecurityDbContext dbContext = _securityDbContextFactory.CreateDbContext();
			var exists = await dbContext.Trucks.AnyAsync(r => r.LicensePlate == value.LicensePlate);

			if (!exists)
			{
				throw new KeyNotFoundException($"Truck with license plate: {value.LicensePlate} not found.");
			}

			Truck existingTruck = await this.GetAsync(value.LicensePlate);

			existingTruck.Container = value.Container;
			existingTruck.SecurityStatus = value.SecurityStatus;

			await dbContext.SaveChangesAsync();

			return existingTruck;
		}

		public async Task DeleteAsync(string plate)
		{
			SecurityDbContext dbContext = _securityDbContextFactory.CreateDbContext();
			Truck truckToDelete = new Truck() { LicensePlate = plate };
			dbContext.Entry(truckToDelete).State = EntityState.Deleted;
			await dbContext.SaveChangesAsync();
		}

		public async Task Update(Truck value)
		{
			SecurityDbContext dbContext = _securityDbContextFactory.CreateDbContext();
			dbContext.Trucks.Update(value);
			await dbContext.SaveChangesAsync();
		}

		public async Task UpdateSecurityStatusAsync(string plate, SecurityStatus securityStatus)
		{
			SecurityDbContext dbContext = _securityDbContextFactory.CreateDbContext();
			Truck existingTruck = await this.GetAsync(plate);

			existingTruck.SecurityStatus = securityStatus;

			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Truck>> GetTrucksNeededToBeCheckedAsync()
		{
			SecurityDbContext dbContext = _securityDbContextFactory.CreateDbContext();
			return await dbContext.Trucks.Where(t => t.SecurityStatus != SecurityStatus.Completed).ToArrayAsync();
		}

		public async Task<Truck> CreateAsync(Truck value)
		{
			SecurityDbContext dbContext = _securityDbContextFactory.CreateDbContext();
			var newTruck = (await dbContext.Trucks.AddAsync(value)).Entity;
			await dbContext.SaveChangesAsync();

			return newTruck;
		}

		public Task<Truck> GetAsync(string plate)
		{
			SecurityDbContext dbContext = _securityDbContextFactory.CreateDbContext();
			return dbContext.Trucks.LastOrDefaultAsync(r => r.LicensePlate == plate);
		}

		public async Task UpdateContainerAsync(string plate, Container container = null)
		{
			Truck existingTruck = await GetAsync(plate);

			existingTruck.Container = container;

			await Update(existingTruck);
		}
	}
}
