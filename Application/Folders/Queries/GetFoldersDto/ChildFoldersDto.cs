using Application.Folders.Queries.GetFoldersDto;
using System.Collections.Generic;

namespace Application.Folders.Queries.GetFolddersDto
{
	public class ChildFoldersDto
	{
		public int ParentFolderId { get; set; }
		public IList<FolderDto> Folders { get; set; }
	}
}
