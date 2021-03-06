﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SecurityService.Core;
using SecurityService.Core.Messaging;
using SecurityService.Core.Repositories;
using SecurityService.Core.Services;
using SecurityService.Infrastructure.Database;
using SecurityService.Infrastructure.Messaging;
using SecurityService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityService.Infrastructure.DI
{
	/// <summary>
	/// Helper for setting up Dependency Injection
	/// </summary>
	public static class DIHelper
	{
		/// <summary>
		/// Setups dependency injection for Infrastructure
		/// </summary>
		public static void Setup(IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton((provider) => new SecurityDbContextFactory(configuration.GetSection("DB_CONNECTION_STRING").Value));

			services.AddTransient<ITruckRepository, TruckRepository>();
			services.AddTransient<ISecurityService, Services.SecurityService>();

			services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>((provider) => new RabbitMQMessageHandler(configuration.GetSection("AMQP_URL").Value));
			services.AddTransient<IMessagePublisher, RabbitMQMessagePublisher>((provider) => new RabbitMQMessagePublisher(configuration.GetSection("AMQP_URL").Value));
		}

		public static void OnServicesSetup(IServiceProvider serviceProvider)
		{
			var dbContextFactory = serviceProvider.GetService<SecurityDbContextFactory>();
			SecurityDbContext dbContext = dbContextFactory.CreateDbContext();
			Policy
				.Handle<Exception>()
				.WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) => { Console.Error.WriteLine("Error connecting to database. Retrying in 5 sec."); })
				.Execute(() =>
				{
					Console.WriteLine("Connecting to database and migrating if required");
					dbContext.Database.Migrate();
					Console.WriteLine("Completed connecting to database");
				});
		}
	}
}
