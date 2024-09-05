// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.IO.Compression;

using HttpShare.Files;
using HttpShare.Models;
using HttpShare.Sessions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;

/// <summary>
/// The default controller class.
/// </summary>
/// <param name="serverSession">The selected <see cref="ServerSession"/> object.</param>
[Controller]
[Route("/")]
public sealed class HomeController(ServerSession serverSession) : Controller
{
	/// <summary>
	/// Handles requests to address /.
	/// </summary>
	[HttpGet]
	[Route("/")]
	public IActionResult Index()
	{
		ViewData["PageTitle"] = $"{serverSession.HostName} - HTTP Share";

		bool sendSession = serverSession is ISendSession,
			receiveSession = serverSession is IReceiveSession;

		ViewData["SendSession"] = sendSession;
		ViewData["ReceiveSession"] = receiveSession;

		if (sendSession) ViewData["OutboxFiles"] = (serverSession as ISendSession)!.OutboxFiles;

		return View();
	}


	/// <summary>
	/// Handles requests to address /Download/.
	/// </summary>
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

	/// <summary>
	/// Handles requests to address /Upload/.
	/// </summary>
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

// TODO Add implementation of the password system in this controller later
