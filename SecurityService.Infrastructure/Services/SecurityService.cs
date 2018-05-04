using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SecurityService.Core;
using SecurityService.Core.Models;
using SecurityService.Core.Repositories;
using SecurityService.Core.Services;

namespace SecurityService.Infrastructure.Services
{
	public class SecurityService : ISecurityService
	{
		private readonly ITruckRepository _truckRepository;

		public SecurityService(ITruckRepository truckRepository)
		{
			_truckRepository = truckRepository;
		}

		public Task CheckTruck(Truck truck)
		{
			// We need to ensure we are saved in the db


		}

		public Task CheckOutstandingChecks()
		{
			throw new NotImplementedException();
		}

		public Task<Truck> SaveTruck(Truck truck)
		{
			throw new NotImplementedException();
		}
	}
}
