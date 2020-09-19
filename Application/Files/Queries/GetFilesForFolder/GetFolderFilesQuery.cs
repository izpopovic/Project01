using Application.Exceptions;
using Application.Files.Queries.GetFilesDto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Files.Queries.GetFilesForFolder
{
	public class GetFolderFilesQuery : IRequest<FilesDto>
	{
		public int FolderId { get; set; }
	}

	public class GetFolderFilesQueryHandler : IRequestHandler<GetFolderFilesQuery, FilesDto>
	{
		private IApplicationDbContext _context;

		public GetFolderFilesQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<FilesDto> Handle(GetFolderFilesQuery request, CancellationToken cancellationToken)
		{
			var folder = await _context.Folders.Include(f => f.Files).FirstOrDefaultAsync(f => f.Id == request.FolderId);
			if (folder == null)
			{
				throw new NotFoundException(nameof(Folder), request.FolderId);
			}

			var folderFiles = folder.Files.ToList().ConvertAll(x => new FileDto { Id = x.Id, Name = x.Name });

			return new FilesDto()
			{
				FolderId = folder.Id,
				Files = folderFiles
			};
		}
	}
}
