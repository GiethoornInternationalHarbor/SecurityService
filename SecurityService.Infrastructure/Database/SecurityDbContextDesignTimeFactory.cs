using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

#if DEBUG
namespace SecurityService.Infrastructure.Database
{
	public class SecurityDbContextDesignTimeFactory : IDesignTimeDbContextFactory<SecurityDbContext>
	{
		public SecurityDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<SecurityDbContext>();
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SecurityService;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new SecurityDbContext(optionsBuilder.Options);
		}
	}

}
#endif