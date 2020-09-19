using Application.Folders.Queries.GetFoldersDto;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folders.Queries.GetFolder
{
	public class GetFolderQuery : IRequest<FolderDto>
	{
		public int FolderId { get; set; }
	}

	public class GetFolderQueryHandler : IRequestHandler<GetFolderQuery, FolderDto>
	{
		private readonly IApplicationDbContext _context;
		public GetFolderQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<FolderDto> Handle(GetFolderQuery request, CancellationToken cancellationToken)
		{
			// Could use mapping engine like AutoMapper, but for simplicity i'll manually map Folder object to FolderDto
			return await _context.Folders.Select(f =>
				new FolderDto()
				{
					Id = f.Id,
					Name = f.Name
				}).SingleOrDefaultAsync(f => f.Id == request.FolderId);
		}
	}
}
