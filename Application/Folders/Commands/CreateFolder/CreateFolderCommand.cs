using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folders.Commands.CreateFolder
{
	public class CreateFolderCommand : IRequest<int>
	{
		public string FolderName { get; set; }

		public int? ParentFolderId { get; set; }
	}

	public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, int>
	{
		private readonly IApplicationDbContext _context;

		public CreateFolderCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<int> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
		{
			var entity = new Folder
			{
				Name = request.FolderName,
				ParentId = request.ParentFolderId
			};

			// If we are adding a child folder
			if (entity.ParentId != null)
			{
				var parentFolder = await _context.Folders.Include(f => f.Children).FirstOrDefaultAsync(f => f.Id == entity.ParentId);
				if (parentFolder != null)
				{
					parentFolder.Children.Add(entity);
				}
				else
				{
					throw new NotFoundException(nameof(Folder), request.ParentFolderId);
				}
			}
			// If we are adding a root level folder
			else
			{
				_context.Folders.Add(entity);
			}

			await _context.SaveChangesAsync(cancellationToken);

			return entity.Id;
		}
	}
}
