using HttpShare.Models;
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

		List<InboxFile> uploadFiles = [];

		foreach (IFormFile file in uploadDataModel.Files)
		{
			using MemoryStream fileStream = new MemoryStream();
			file.CopyTo(fileStream);

			InboxFile tempIndexFile = new InboxFile(uploadDataModel.DisplayName,
				file.FileName, fileStream.ToArray());
			uploadFiles.Add(tempIndexFile);

			fileStream.Flush();
		}

		(serverSession as IReceiveSession)!.InvokeReceivedFilesEvent(uploadFiles);
		return Redirect("/");
	}
}
