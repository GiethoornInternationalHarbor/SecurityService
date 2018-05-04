using SecurityService.Core.Messaging;
using SecurityService.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
						break;
					}
			}
			
			return true;
		}

		private async Task<bool> HandleTruckArriving()
		{

		}

		private async Task<bool> HandleTruckDeparting()
		{

		}
	}
}
