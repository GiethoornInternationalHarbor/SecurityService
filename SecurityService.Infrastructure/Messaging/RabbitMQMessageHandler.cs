using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SecurityService.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityService.Infrastructure.Messaging
{
	public class RabbitMQMessageHandler : IMessageHandler
	{
		private Uri _uri;
		private string _exchange;
		private string _queuename;
		private IConnection _connection;
		private IModel _model;
		private EventingBasicConsumer _consumer;
		private string _consumerTag;
		private IMessageHandlerCallback _callback;

		public RabbitMQMessageHandler(string uri, string exchange = RabbitMQMessageExchanges.Default, string queuename = RabbitMQMessageQueues.Default)
		{
			_uri = new Uri(uri);
			_exchange = exchange;
			_queuename = queuename;
		}

		public void Start(IMessageHandlerCallback callback)
		{
			_callback = callback;

			Policy
				.Handle<Exception>()
				.WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) => { Console.WriteLine("Error connecting to RabbitMQ. Retrying in 5 sec."); })
				.Execute(() =>
				{
					var factory = new ConnectionFactory() { Uri = _uri };
					_connection = factory.CreateConnection();
					_model = _connection.CreateModel();
					_model.ExchangeDeclare(_exchange, "fanout", durable: true, autoDelete: false);
					_model.QueueDeclare(_queuename, durable: true, autoDelete: false, exclusive: false);
					_model.QueueBind(_queuename, _exchange, "");
					_consumer = new EventingBasicConsumer(_model);
					_consumer.Received += Consumer_Received;
					_consumerTag = _model.BasicConsume(_queuename, false, _consumer);
				});
		}

		public void Stop()
		{
			_model.BasicCancel(_consumerTag);
			_model.Close(200, "Goodbye");
			_connection.Close();
		}

		private async void Consumer_Received(object sender, BasicDeliverEventArgs ea)
		{
			if (await HandleEvent(ea))
			{
				_model.BasicAck(ea.DeliveryTag, false);
			}
		}

		private async Task<bool> HandleEvent(BasicDeliverEventArgs ea)
		{
			// determine messagetype
			string messageTypeString = ea.BasicProperties.Type;
			MessageTypes messageType = MessageTypes.Unknown;
			Enum.TryParse<MessageTypes>(messageTypeString, out messageType);

			if (messageType == MessageTypes.Unknown)
			{
				// This service cant handle that
				return true;
			}

			// get body
			string body = Encoding.UTF8.GetString(ea.Body);

			// call callback to handle the message
			return await _callback.HandleMessageAsync(messageType, body);
		}
	}
}