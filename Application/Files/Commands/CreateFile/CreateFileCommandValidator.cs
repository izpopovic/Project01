using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Files.Commands.CreateFile
{
	public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
	{
		public CreateFileCommandValidator()
		{
			RuleFor(f => f.FileName)
				.MaximumLength(30)
				.NotEmpty();

			RuleFor(f => f.FolderId)
				.NotNull()
				.NotEmpty().WithMessage("FolderId is required!");
		}
	}
}
