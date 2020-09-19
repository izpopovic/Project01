using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folders.Commands.UpdateFolder
{
	public class UpdateFolderCommand : IRequest
	{
		public int Id { get; set; }
		public string FolderName { get; set; }
	}

	public class UpdateFolderCommandHandler : IRequestHandler<UpdateFolderCommand>
	{
		private readonly IApplicationDbContext _context;

		public UpdateFolderCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
		{
			var entity = await _context.Folders.FindAsync(request.Id);

			if (entity == null)
			{
				throw new NotFoundException(nameof(Folder), request.Id);
			}

			entity.Name = request.FolderName;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
