using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityService.Core.Messaging
{
	public interface IMessageHandler
	{
		void Start(IMessageHandlerCallback callback);
		void Stop();
	}
}
