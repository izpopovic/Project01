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
			var folder = await _context.Folders.Include(f => f.Files).FirstOrDefaultAsync(f => f.Id == request.Id);

			if (folder == null)
			{
				throw new NotFoundException(nameof(Folder), request.Id);
			}

			var files = folder.Files;
			if (files.Count > 0)
			{
				_context.Files.RemoveRange(files);
			}

			_context.Folders.Remove(folder);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
