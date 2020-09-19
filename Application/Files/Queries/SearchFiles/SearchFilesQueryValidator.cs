using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Files.Queries.SearchFiles
{
	public class SearchFilesQueryValidator : AbstractValidator<SearchFilesQuery>
	{
		public SearchFilesQueryValidator()
		{
			RuleFor(f => f.SearchText)
				.NotNull()
				.NotEmpty().WithMessage("SearchText is required.");
		}
	}
}
