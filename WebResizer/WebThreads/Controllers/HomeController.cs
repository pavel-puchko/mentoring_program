using System.Web.Mvc;
using WebThreads.Models;
using WebThreads.Services;

namespace WebThreads.Controllers
{
	public class HomeController : Controller
	{
		private readonly ThreadsResizer _threadsResizer;

		public HomeController()
		{
			_threadsResizer = new ThreadsResizer();
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
			_threadsResizer.ThreadResize(options);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult StopResize()
		{
			_threadsResizer.StopResizing();

			return Json("Stopped!");
		}
	}
}