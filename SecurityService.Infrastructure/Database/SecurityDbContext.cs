using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SecurityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityService.Infrastructure.Database
{
	public class SecurityDbContext : DbContext
	{
		public DbSet<Truck> Trucks { get; set; }

		public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
		{
		}
	}
}
