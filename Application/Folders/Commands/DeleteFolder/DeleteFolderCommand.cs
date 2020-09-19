using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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
			var entity = await _context.Folders.FindAsync(request.Id);

			if (entity == null)
			{
				throw new NotFoundException(nameof(Folder), request.Id);
			}

			_context.Folders.Remove(entity);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
