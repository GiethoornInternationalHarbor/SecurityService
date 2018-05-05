namespace SecurityService.Core.Messaging
{
	public enum MessageTypes
	{
		Unknown,
		TruckArrivingEvent,
		TruckDepartingEvent,
		TruckArrivedEvent,
		TruckDepartedEvent,
		ShipContainerLoadedEvent,
		ShipContainerUnloadedEvent,
		TruckClearedEvent
	}
}