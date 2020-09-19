using FluentValidation;

namespace Application.Files.Commands.UpdateFile
{
	public class UpdateFileCommandValidator : AbstractValidator<UpdateFileCommand>
	{
		public UpdateFileCommandValidator()
		{
			RuleFor(v => v.FileName)
				.MaximumLength(200)
				.NotEmpty();
		}
	}
}
