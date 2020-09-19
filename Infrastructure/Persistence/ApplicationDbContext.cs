using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
	class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Folder> Folders { get; set; }

		public DbSet<File> Files { get; set; }


		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			int result = await base.SaveChangesAsync(cancellationToken);

			return result;
		}
	}
}
