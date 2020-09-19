using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Files.Commands.UpdateFile
{
	public class UpdateFileCommand : IRequest
	{
		public int Id { get; set; }
		public string FileName { get; set; }
	}

	public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand>
	{
		private readonly IApplicationDbContext _context;

		public UpdateFileCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Unit> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
		{
			var entity = await _context.Files.FindAsync(request.Id);

			if (entity == null)
			{
				throw new NotFoundException(nameof(File), request.Id);
			}

			entity.Name = request.FileName;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
