namespace Application.Folders.Queries.GetFoldersDto
{
	// Data transfer object, things we return back to client for it to see it, here we can set limitations to what he sees and use automapper to map certain proiperties
	public class FolderDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
