using System.Collections.Generic;

namespace Application.Files.Queries.GetFilesDto
{
	public class FilesDto
	{
		public int FolderId { get; set; }
		public IList<FileDto> Files { get; set; }
	}
}
