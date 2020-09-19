using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folders.Queries.GetChildFolders
{
	public class GetChildFoldersQueryValidator : AbstractValidator<GetChildFoldersQuery>
	{
		public GetChildFoldersQueryValidator()
		{
			RuleFor(f => f.ParentFolderId)
				.NotNull()
				.NotEmpty().WithMessage("ParentFolderId is required");
		}
	}
}
