﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecurityService.App;
using SecurityService.Core.Messaging;
using SecurityService.Infrastructure.DI;
using System;
using System.IO;
using System.Threading;

namespace SecurityService
{
	class Program
	{
		public static IServiceProvider ServiceProvider { get; private set; }

		static Program()
		{
			DotNetEnv.Env.Load();

			//setup our DI
			var serviceCollection = new ServiceCollection();

			ConfigureServices(serviceCollection);

			ServiceProvider = serviceCollection.BuildServiceProvider();

			DIHelper.OnServicesSetup(ServiceProvider);
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			IConfiguration config = new ConfigurationBuilder()
				.AddEnvironmentVariables()
				.Build();

			DIHelper.Setup(services, config);

			// Setup the app services
			services.AddTransient<IMessageHandlerCallback, SecurityMessageHandler>();
		}

		static void Main(string[] args)
		{
			// Get the message handler
			IMessageHandler messageHandler = ServiceProvider.GetService<IMessageHandler>();
			IMessageHandlerCallback messageHandlerCallback = ServiceProvider.GetService<IMessageHandlerCallback>();

			try
			{
				Console.WriteLine("Starting handler");
				messageHandler.Start(messageHandlerCallback);
				Console.WriteLine("Handler started");
			}
			catch (Exception ex)
			{
				// Error during staring
				Console.Error.WriteLine($"Error during starting message handler, message: {ex.Message}");
				Environment.Exit(1);
			}

			Console.WriteLine("Security service started.");
			while (true)
			{
				Thread.Sleep(10000);
			}
		}
	}
}
