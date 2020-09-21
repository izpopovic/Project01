using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folders.Commands.DeleteFolder
{
	public class DeleteFolderCommand : IRequest
	{
		public int Id { get; set; }
	}

	public class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand>
	{
		private readonly IApplicationDbContext _context;

		public DeleteFolderCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
		{

			var folder = await _context.Folders.Include(f => f.Children).ThenInclude(f => f.Files).FirstOrDefaultAsync(f => f.Id == request.Id);

			if (folder == null)
			{
				throw new NotFoundException(nameof(Folder), request.Id);
			}

			await RemoveChildren(folder.Id);

			if (folder.Files != null && folder.Files.Count > 0)
			{
				_context.Files.RemoveRange(folder.Files);
			}

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}

		public async Task RemoveChildren(int i)
		{
			var childrenFolders = await _context.Folders.Include(f => f.Children).ThenInclude(f => f.Files).Where(f => f.ParentId == i).ToListAsync();

			foreach (var child in childrenFolders)
			{
				await RemoveChildren(child.Id);
				foreach (var file in child.Files)
				{
					_context.Files.Remove(file);
				}
				_context.Folders.Remove(child);
			}
		}
	}
}
