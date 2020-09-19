using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
	public class FolderConfiguration : IEntityTypeConfiguration<Folder>
	{
		public void Configure(EntityTypeBuilder<Folder> builder)
		{
			builder.Property(f => f.Name)
				.HasMaxLength(30)
				.IsRequired();
		}
	}
}
