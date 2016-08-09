using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebThreads.Models;
using WebThreads.Services;

namespace WebThreads.Controllers
{
	public class HomeController : Controller
	{
		private readonly ImageService imageService;

		public HomeController()
		{
			imageService = new ImageService();
		}

		public ActionResult Index()
		{
			Options options = new Options();

			options.ThreadsNumber = 3;
			options.InputFolder = "F:\\epammentoring\\tasks\\input";
			options.OutputFolder = "F:\\epammentoring\\tasks\\output";
			options.Sizes = "200x200; 300x300";

            return View(options);
		}

		[HttpPost]
		public ActionResult Index(Options options)
		{
			string[] Extensions = new string[] { "*.jpg", "*.jpeg", "*.png", "*.gif" };
			string[] ImageSizes = options.Sizes.Split(';');
			ThreadPool.SetMaxThreads(options.ThreadsNumber, options.ThreadsNumber);

			if (!Directory.Exists(options.OutputFolder))
			{
				Directory.CreateDirectory(options.OutputFolder);
			}

			string[] paths = imageService.FindImages(options.InputFolder, Extensions);
			int count = paths.Length;
			ManualResetEvent resizeFinished = new ManualResetEvent(false);

			foreach (var path in paths)
			{
				ThreadPool.QueueUserWorkItem((o) => {
					string FileName = Path.GetFileName(path);
					Image ImgFromPath = imageService.GetImage(FileName, options.InputFolder);

					foreach (var size in ImageSizes)
					{
						imageService.SaveImg(imageService.ResizeImage(ImgFromPath, size), Path.Combine(options.OutputFolder, size + "_" + FileName));
					}


					if (Interlocked.Decrement(ref count) == 0)
					{
						resizeFinished.Set();
					}

				});
			}
			WaitHandle.WaitAll(new ManualResetEvent[] { resizeFinished });

			return RedirectToAction("Index");
		}
	}
}