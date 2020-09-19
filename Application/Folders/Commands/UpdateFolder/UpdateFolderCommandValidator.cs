using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folders.Commands.UpdateFolder
{
	class UpdateFolderCommandValidator : AbstractValidator<UpdateFolderCommand>
	{
		public UpdateFolderCommandValidator()
		{
			RuleFor(f => f.FolderName)
				.MaximumLength(30)
				.NotEmpty();
		}
	}
}
