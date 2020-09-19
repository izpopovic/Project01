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

namespace Application.Files.Queries.SearchFiles
{
	public class SearchFilesQuery : IRequest<FilesDto>
	{
		public string SearchText { get; set; }
		public int? FolderId { get; set; }
	}

	public class SearchFilesQueryHandler : IRequestHandler<SearchFilesQuery, FilesDto>
	{
		private IApplicationDbContext _context;

		public SearchFilesQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<FilesDto> Handle(SearchFilesQuery request, CancellationToken cancellationToken)
		{

			// If we are searching inside of parent folder
			if (request.FolderId != null)
			{
				var folder = await _context.Folders.Include(f => f.Files).FirstOrDefaultAsync(f => f.Id == request.FolderId);
				if (folder == null)
				{
					throw new NotFoundException(nameof(Folder), request.FolderId);
				}

				var foundFiles = folder.Files.Where(f => f.Name.StartsWith(request.SearchText));

				var files = foundFiles.ToList().ConvertAll(x => new FileDto { Id = x.Id, Name = x.Name });

				return new FilesDto()
				{
					FolderId = folder.Id,
					Files = files
				};
			}
			// If we are searching across all folders
			else
			{
				var foundFiles = await _context.Files.Where(f => f.Name.StartsWith(request.SearchText)).ToListAsync();

				var files = foundFiles.ToList().ConvertAll(x => new FileDto { Id = x.Id, Name = x.Name });

				return new FilesDto()
				{
					Files = files
				};
			}
		}
	}
}
