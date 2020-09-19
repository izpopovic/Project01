using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folders.Queries.GetFolder
{
	public class GetFolderQueryValidation : AbstractValidator<GetFolderQuery>
	{
		public GetFolderQueryValidation()
		{
			RuleFor(f => f.FolderId)
				.NotNull()
				.NotEmpty().WithMessage("FolderId is required.");
		}
	}
}
