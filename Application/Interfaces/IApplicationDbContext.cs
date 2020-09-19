using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<Folder> Folders { get; set; }
		DbSet<File> Files { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
