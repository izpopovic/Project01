using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configuration
{
	public class FilesConfiguration : IEntityTypeConfiguration<File>
	{
		public void Configure(EntityTypeBuilder<File> builder)
		{
			builder.Property(f => f.Name)
				.HasMaxLength(30)
				.IsRequired();
		}
	}
}
