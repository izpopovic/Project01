using Application.Folders.Commands.CreateFolder;
using Application.Folders.Commands.DeleteFolder;
using Application.Folders.Commands.UpdateFolder;
using Application.Folders.Queries.GetChildFolders;
using Application.Folders.Queries.GetFolddersDto;
using Application.Folders.Queries.GetFolder;
using Application.Folders.Queries.GetFoldersDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FoldersController : ApiController
	{
		//GetParentFolder?

		[HttpGet("[action]/{id}")]
		public async Task<ActionResult<ChildFoldersDto>> GetChildFolders(int id)
		{
			return await Mediator.Send(new GetChildFoldersQuery {ParentFolderId = id });
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<FolderDto>> GetFolder(int id)
		{
			return await Mediator.Send(new GetFolderQuery { FolderId = id });
		}

		[HttpPost]
		public async Task<ActionResult<int>> Create(CreateFolderCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<int>> Update(int id, UpdateFolderCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}

			await Mediator.Send(command);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await Mediator.Send(new DeleteFolderCommand { Id = id });

			return NoContent();
		}
	}
}
