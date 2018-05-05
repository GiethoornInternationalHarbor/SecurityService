using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
			services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(configuration.GetSection("DB_CONNECTION_STRING").Value));

			services.AddTransient<ITruckRepository, TruckRepository>();
			services.AddTransient<ISecurityService, Services.SecurityService>();

			services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>((provider) => new RabbitMQMessageHandler(configuration.GetSection("AMQP_URL").Value));
		}

		public static void OnServicesSetup(IServiceProvider serviceProvider)
		{
			Console.WriteLine("Connecting to database and migrating if required");
			var dbContext = serviceProvider.GetService<SecurityDbContext>();
			dbContext.Database.Migrate();
			Console.WriteLine("Completed connecting to database");
		}
	}
}
