using SecurityService.Core.Messaging;
using SecurityService.Core.Models;
using SecurityService.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;

namespace SecurityService.App.Messaging
{
	public class SecurityMessageHandler : IMessageHandlerCallback
	{
		private readonly ISecurityService _securityService;
		public SecurityMessageHandler(ISecurityService securityService)
		{
			_securityService = securityService;
		}

		public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
		{
			switch (messageType)
			{
				case MessageTypes.TruckArrivingEvent:
					{
						return await HandleTruckArriving(message);
					}
				case MessageTypes.TruckDepartingEvent:
					{
						return await HandleTruckDeparting(message);
					}
				case MessageTypes.ShipContainerLoadedEvent:
					{
						return await HandleShipContainerLoaded(message);
					}
				case MessageTypes.ShipContainerUnloadedEvent:
					{
						return await HandleShipContainerUnloaded(message);
					}
			}

			return true;
		}

		private async Task<bool> HandleTruckArriving(string message)
		{
			// Deserialize the message as a truck
			Truck receivedTruck = JsonSerializer.Deserialize<Truck>(message);

			// Ensure the status is correct
			receivedTruck.SecurityStatus = SecurityStatus.NotStarted;

			// Because we are arriving the truck is not yet created in the db
			Truck createdTruck = await _securityService.CreateTruckAsync(receivedTruck);

			// Now the truck is in the repo, and in the background the checking happens
			Task.Run(() => _securityService.CheckTruckAsync(createdTruck));

			return true;
		}

		private async Task<bool> HandleTruckDeparting(string message)
		{
			// Deserialize the message as a truck
			Truck receivedTruck = JsonSerializer.Deserialize<Truck>(message);

			// We should already have a truck in our db
			Truck existingTruck = await _securityService.GetTruckAsync(receivedTruck.LicensePlate);
			existingTruck.SecurityStatus = SecurityStatus.NotStarted;

			await _securityService.SaveTruckAsync(existingTruck);

			// Now the truck is in the repo, and in the background the checking happens
			Task.Run(() => _securityService.CheckTruckAsync(existingTruck));

			return true;
		}

		private async Task<bool> HandleShipContainerLoaded(string message)
		{
			// TODO: Needs correcting because unknown what the event shall contain
			Truck truck = JsonSerializer.Deserialize<Truck>(message);
			// When a container is loaded on the ship, it means it is removed from the truck
			await _securityService.UpdateTruckContainerAsync(truck.LicensePlate);

			return true;
		}

		private async Task<bool> HandleShipContainerUnloaded(string message)
		{
			// TODO: Needs correcting because unknown what the event shall contain
			Truck truck = JsonSerializer.Deserialize<Truck>(message);

			// When a container is unloaded off the ship, it means it is put on the truck
			await _securityService.UpdateTruckContainerAsync(truck.LicensePlate, truck.Container);

			return true;
		}
	}
}
