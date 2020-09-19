using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Files.Commands.DeleteFile
{
	public class DeleteFileCommand : IRequest
	{
		public int Id { get; set; }
	}

	public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
	{
		private readonly IApplicationDbContext _context;

		public DeleteFileCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
		{
			var entity = await _context.Files.FindAsync(request.Id);

			if (entity == null)
			{
				throw new NotFoundException(nameof(File), request.Id);
			}

			_context.Files.Remove(entity);

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
