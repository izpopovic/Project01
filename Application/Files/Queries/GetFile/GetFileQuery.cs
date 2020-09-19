using Application.Files.Queries.GetFilesDto;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Files.Queries.GetFile
{
	public class GetFileQuery : IRequest<FileDto>
	{
		public int FileId { get; set; }
	}

	public class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileDto>
	{
		private IApplicationDbContext _context;

		public GetFileQueryHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<FileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
		{
			return await _context.Files.Select(f =>
				new FileDto()
				{
					Id = f.Id,
					Name = f.Name
				}).SingleOrDefaultAsync(f => f.Id == request.FileId);
		}
	}
}
