using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace HttpShare.Controllers;


[Controller]
[Route("/")]
public sealed class HomeController(ServerSession serverSession) : Controller
{
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

	[HttpGet]
	[Route("/Download/")]
	public IActionResult Download()
	{
		bool sendSession = serverSession is ISendSession;

		if (!sendSession) return NotFound();

		byte[] zipData = [0];
		MemoryStream memoryStream = new MemoryStream();

		using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
		{
			ICollection<File> outboxFiles = (serverSession as ISendSession)!.OutboxFiles;

			foreach (File file in outboxFiles)
			{
				using Stream fileStream = zipArchive.CreateEntry(file.Filename).Open();
				fileStream.Write(file.Data);
				fileStream.Flush();
			}
		}

		zipData = memoryStream.ToArray();

		memoryStream.Flush();
		memoryStream.Dispose();

		return File(zipData, "application/zip-compressed",
			$"HttpShare_{DateTime.Now:yyyyMMdd_HHmmss}.zip");
	}

	// TODO Convert the args for this route into a data model
	[HttpPost]
	[Route("/Upload/")]
	public IActionResult Upload([FromForm] ICollection<IFormFile> files, [FromForm] string senderName)
	{
		bool isReceiveSession = serverSession is IReceiveSession;
		if (!isReceiveSession) return NotFound();

		List<InboxFile> uploadFiles = [];

		foreach (IFormFile file in files)
		{
			using MemoryStream fileStream = new MemoryStream();
			file.CopyTo(fileStream);

			uploadFiles.Add(new InboxFile(senderName, file.FileName, fileStream.ToArray()));

			fileStream.Flush();
		}

		(serverSession as IReceiveSession)!.InvokeFilesReceived(uploadFiles);
		return Redirect("/");
	}
}
