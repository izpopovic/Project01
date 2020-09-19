using Application.Exceptions;
using Application.Folders.Queries.GetFolddersDto;
using Application.Folders.Queries.GetFoldersDto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folders.Queries.GetChildFolders
{
	public class GetChildFoldersQuery : IRequest<ChildFoldersDto>
	{
		public int ParentFolderId { get; set; }
	}

	public class GetChildFolderQueryHandler : IRequestHandler<GetChildFoldersQuery, ChildFoldersDto>
	{
		private readonly IApplicationDbContext _context;
		public GetChildFolderQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<ChildFoldersDto> Handle(GetChildFoldersQuery request, CancellationToken cancellationToken)
		{
			var folder = await _context.Folders.Include(f => f.Children).FirstOrDefaultAsync(f => f.Id == request.ParentFolderId);
			if (folder == null)
			{
				throw new NotFoundException(nameof(Folder), request.ParentFolderId);
				//return null;
			}

			var childfolders = folder.Children.ToList().ConvertAll(x => new FolderDto { Id = x.Id, Name = x.Name });

			return new ChildFoldersDto()
			{
				ParentFolderId = request.ParentFolderId,
				Folders = childfolders
			};

		}
	}
}
