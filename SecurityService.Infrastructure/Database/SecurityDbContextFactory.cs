using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityService.Infrastructure.Database
{
	public class SecurityDbContextFactory
	{
		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		protected string ConnectionString { get; set; }

		public SecurityDbContextFactory(string connectionString)
		{
			ConnectionString = connectionString;
		}

		/// <summary>
		/// Creates the database context.
		/// </summary>
		/// <returns></returns>
		public SecurityDbContext CreateDbContext()
		{
			var optBuilder = new DbContextOptionsBuilder<SecurityDbContext>();
			optBuilder.UseSqlServer(ConnectionString);

			return new SecurityDbContext(optBuilder.Options);
		}
	}
}
