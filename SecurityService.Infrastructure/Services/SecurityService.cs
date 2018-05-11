using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SecurityService.Core;
using SecurityService.Core.Messaging;
using SecurityService.Core.Models;
using SecurityService.Core.Repositories;
using SecurityService.Core.Services;

namespace SecurityService.Infrastructure.Services
{
	public class SecurityService : ISecurityService
	{
		private const int CHECK_DURATION = 30000;
		private readonly ITruckRepository _truckRepository;
		private readonly IMessagePublisher _messagePublisher;

		public SecurityService(ITruckRepository truckRepository, IMessagePublisher messagePublisher)
		{
			_truckRepository = truckRepository;
			_messagePublisher = messagePublisher;
			CheckOutstandingChecksAsync();
		}

		public Task CheckTruckAsync(Truck truck)
		{
			// If we do not have a container, we take only half of the time
			return Task.Run(async () =>
			{
				truck.SecurityStatus = SecurityStatus.InProgress;
				await _truckRepository.UpdateSecurityStatusAsync(truck.LicensePlate, SecurityStatus.InProgress);
				await Task.Delay(truck.Container == null ? CHECK_DURATION / 2 : CHECK_DURATION);
				truck.SecurityStatus = SecurityStatus.Completed;
				await _truckRepository.UpdateSecurityStatusAsync(truck.LicensePlate, SecurityStatus.Completed);

				await _messagePublisher.PublishMessageAsync(MessageTypes.TruckClearedEvent, truck);
			});
		}

		public Task CheckOutstandingChecksAsync()
		{
			return Task.Run(async () =>
			{
				// This function should only be called once
				IEnumerable<Truck> trucks = await _truckRepository.GetTrucksNeededToBeCheckedAsync();

				IEnumerable<Task> tasks = trucks.Select(t => CheckTruckAsync(t));
				await Task.WhenAll(tasks);
			});
		}

		public Task<Truck> SaveTruckAsync(Truck truck)
		{
			return _truckRepository.UpdateAsync(truck);
		}

		public Task<Truck> CreateTruckAsync(Truck truck)
		{
			return _truckRepository.CreateAsync(truck);
		}

		public Task UpdateTruckContainerAsync(string plate, Container container = null)
		{
			return _truckRepository.UpdateContainerAsync(plate, container);
		}

		public Task<Truck> GetTruckAsync(string plate)
		{
			return _truckRepository.GetAsync(plate);
		}
	}
}
