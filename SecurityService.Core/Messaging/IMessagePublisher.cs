using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityService.Core.Messaging
{
	public interface IMessagePublisher
	{
		/// <summary>
		/// Publishes the message asynchronous.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		Task PublishMessageAsync<T>(MessageTypes messageType, T message);
	}

}
