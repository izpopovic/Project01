using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Files.Commands.CreateFile
{
	public class CreateFileCommand : IRequest<int>
	{
		public string FileName { get; set; }
		public int FolderId { get; set; }
	}

	public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, int>
	{
		private IApplicationDbContext _context;

		public CreateFileCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<int> Handle(CreateFileCommand request, CancellationToken cancellationToken)
		{
			var entity = new File()
			{
				Name = request.FileName
			};

			var folder = await _context.Folders.Include(f => f.Files).FirstOrDefaultAsync(f => f.Id == request.FolderId);
			if (folder == null)
			{
				throw new NotFoundException(nameof(Folder), request.FolderId);
			}
			folder.Files.Add(entity);

			await _context.SaveChangesAsync(cancellationToken);

			return entity.Id;
		}
	}
}
