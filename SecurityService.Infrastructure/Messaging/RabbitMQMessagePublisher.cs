using Polly;
using RabbitMQ.Client;
using SecurityService.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;

namespace SecurityService.Infrastructure.Messaging
{
	public class RabbitMQMessagePublisher : IMessagePublisher
	{
		private Uri _uri;
		private string _exchange;

		public RabbitMQMessagePublisher(string uri, string exchange = RabbitMQMessageExchanges.Default)
		{
			_uri = new Uri(uri);
			_exchange = exchange;
		}

		public Task PublishMessageAsync<T>(MessageTypes messageType, T message)
		{
			return Task.Run(() =>
				Policy
					.Handle<Exception>()
					.WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) => { Console.WriteLine("Error connecting to RabbitMQ. Retrying in 5 sec."); })
					.Execute(() =>
					{
						var factory = new ConnectionFactory() { Uri = _uri };
						using (var connection = factory.CreateConnection())
						{
							using (var model = connection.CreateModel())
							{
								model.ExchangeDeclare(_exchange, "fanout", durable: true, autoDelete: false);
								var data = JsonSerializer.Serialize(message);
								IBasicProperties properties = model.CreateBasicProperties();
								properties.Type = messageType.ToString();
								model.BasicPublish(_exchange, "", properties, data);
							}
						}
					}));
		}
	}
}