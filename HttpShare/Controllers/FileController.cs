using System.IO.Compression;

using HttpShare.Files;
using HttpShare.Models;
using HttpShare.Sessions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;

[Controller]
[Authorize(Policy = Constants.LoggedInUsersOnlyPolicy)]
public sealed class FileController(ServerSession serverSession) : Controller
{
	[HttpGet]
	[Route("/Download/")]
	public IActionResult Download()
	{
		bool sendSession = serverSession is ISendSession;

		if (!sendSession) return NotFound();

		MemoryStream memoryStream = new MemoryStream();

		using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
		{
			IEnumerable<IOutboxFile> outboxFiles = (serverSession as ISendSession)!.OutboxFiles;

			foreach (IOutboxFile file in outboxFiles)
			{
				using Stream fileStream = zipArchive.CreateEntry(file.Name).Open();
				fileStream.Write(file.Data);
				fileStream.Flush();
			}
		}

		byte[] zipData = memoryStream.ToArray();

		memoryStream.Flush();
		memoryStream.Dispose();

		return File(zipData, "application/zip-compressed",
			$"HttpShare_{DateTime.Now:yyyyMMdd_HHmmss}.zip");
	}


	[HttpPost]
	[Route("/Upload/")]
	public IActionResult Upload([FromForm] UploadDataModel uploadDataModel)
	{
		bool isReceiveSession = serverSession is IReceiveSession;
		if (!isReceiveSession) return NotFound();

		List<IInboxFile> uploadFiles = [];

		foreach (IFormFile file in uploadDataModel.Files)
		{
			using MemoryStream fileStream = new MemoryStream();
			file.CopyTo(fileStream);

			InboxFile tempIndexFile = new InboxFile(uploadDataModel.DisplayName, file.FileName, fileStream.ToArray());
			uploadFiles.Add(tempIndexFile);

			fileStream.Flush();
		}

		(serverSession as IReceiveSession)!.InvokeReceivedFilesEvent(uploadFiles);
		return Redirect("/");
	}
}
