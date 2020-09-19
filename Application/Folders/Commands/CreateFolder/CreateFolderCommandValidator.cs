using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folders.Commands.CreateFolder
{
	class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
	{
		public CreateFolderCommandValidator()
		{
			RuleFor(f => f.FolderName)
				.MaximumLength(30)
				.NotEmpty();
		}
	}
}
