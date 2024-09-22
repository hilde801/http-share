using System.IO.Compression;
using System.Security.Claims;

using HttpShare.Files;
using HttpShare.Models;
using HttpShare.Sessions;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;

[Controller]
[Authorize(Policy = Constants.LoggedInUsersOnlyPolicy,
	AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public sealed class FileController(ServerSession serverSession) : CustomController(serverSession)
{
	[HttpGet]
	[Route("/Download/")]
	public IActionResult Download()
	{
		bool sendSession = ServerSession is ISendSession;

		if (!sendSession) return NotFound();

		MemoryStream memoryStream = new MemoryStream();

		using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
		{
			IEnumerable<IOutboxFile> outboxFiles = (ServerSession as ISendSession)!.OutboxFiles;

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

		string displayName = User.Claims.First(claim => claim.Type == ClaimTypes.Name).Value,
			message = $"File download to {displayName}.";

		ServerSession.InvokeServerEvent(new ServerSessionEvent(ServerEventType.Information, message));

		return File(zipData, "application/zip-compressed",
			$"HttpShare_{DateTime.Now:yyyyMMdd_HHmmss}.zip");
	}


	[HttpPost]
	[Route("/Upload/")]
	public IActionResult Upload([FromForm] UploadDataModel uploadDataModel)
	{
		bool isReceiveSession = ServerSession is IReceiveSession;
		if (!isReceiveSession) return NotFound();

		List<IInboxFile> uploadFiles = [];

		string displayName = User.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

		foreach (IFormFile file in uploadDataModel.Files)
		{
			using MemoryStream fileStream = new MemoryStream();
			file.CopyTo(fileStream);

			InboxFile tempIndexFile = new InboxFile(displayName, file.FileName, fileStream.ToArray());
			uploadFiles.Add(tempIndexFile);

			fileStream.Flush();
		}

		string message = $"File upload ({uploadFiles.Count}) from {displayName}.";

		ServerSession.InvokeServerEvent(new ServerSessionEvent(ServerEventType.Information, message));

		(ServerSession as IReceiveSession)!.InvokeReceivedFilesEvent(uploadFiles);
		return Redirect("/");
	}
}
