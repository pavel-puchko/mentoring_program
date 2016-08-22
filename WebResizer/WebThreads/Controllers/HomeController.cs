using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebThreads.Controllers;
using WebThreads.Models;
using WebThreads.Services;

namespace WebThreads.Controllers
{
	public class HomeController : Controller
	{
		private readonly ThreadsResizer _threadsResizer;

		public HomeController()
		{
			_threadsResizer = ThreadsResizer.Instance;
		}

		public ActionResult Index()
		{
			Options options = new Options();

			options.ThreadsNumber = 3;
			options.OutputFolder = "C:\\Users\\Pavel_Puchko\\Documents\\mentoring_program\\out";
			options.Sizes = "200x200; 300x300";

            return View(options);
		}

		[HttpPost]
		public ActionResult Index(IEnumerable<HttpPostedFileBase> files, Options options)
		{
			options.InputFolder = Server.MapPath("/uploads");
		
			DirectoryInfo outputDir = new DirectoryInfo(options.OutputFolder);
			DirectoryInfo uploadDir = new DirectoryInfo(options.InputFolder);

			foreach (FileInfo file in outputDir.GetFiles())
			{
				file.Delete();
			}

			foreach (FileInfo file in uploadDir.GetFiles())
			{
				file.Delete();
			}

			List<string> fileNames = new List<string>();

			foreach (var file in files)
			{
				if (file != null && file.ContentLength > 0)
				{
					var fileName = file.FileName;
					fileNames.Add(fileName);
					file.SaveAs(Path.Combine(options.InputFolder, fileName));
				}
			}

			_threadsResizer.ThreadResize(options);

			return Json(fileNames.ToArray());
		}

		[HttpPost]
		public ActionResult StopResize()
		{
			_threadsResizer.StopResizing();

			return Json("Stopped!");
		}

		[HttpGet]
		public ActionResult GetProcessedFiles()
		{
			var files = _threadsResizer.GetProcessedFiles();

			return Json(files.ToArray(), JsonRequestBehavior.AllowGet);
		}
	}
}