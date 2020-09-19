using Application.Files.Commands.CreateFile;
using Application.Files.Commands.DeleteFile;
using Application.Files.Commands.UpdateFile;
using Application.Files.Queries.GetFile;
using Application.Files.Queries.GetFilesDto;
using Application.Files.Queries.GetFilesForFolder;
using Application.Files.Queries.SearchFiles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilesController : ApiController
	{
		// GET: api/<FilesController>
		[HttpGet("{id}")]
		public async Task<ActionResult<FileDto>> GetFile(int id)
		{
			return await Mediator.Send(new GetFileQuery { FileId = id });
		}

		// GET api/<FilesController>/5
		[HttpGet("[action]/{id}")]
		public async Task<ActionResult<FilesDto>> GetFolderFiles(int id)
		{
			return await Mediator.Send(new GetFolderFilesQuery { FolderId = id });
		}

		[HttpGet]
		public async Task<ActionResult<FilesDto>> SearchFiles([FromQuery] SearchFilesQuery query)
		{
			return await Mediator.Send(query);
		}

		// POST api/<FilesController>
		[HttpPost]
		public async Task<ActionResult<int>> Create(CreateFileCommand command)
		{
			return await Mediator.Send(command);
		}

		// PUT api/<FilesController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, UpdateFileCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}

			await Mediator.Send(command);

			return NoContent();
		}

		// DELETE api/<FilesController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await Mediator.Send(new DeleteFileCommand { Id = id });

			return NoContent();
		}
	}
}
