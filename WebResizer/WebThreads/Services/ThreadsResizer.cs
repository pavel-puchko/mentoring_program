using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using WebThreads.Models;

namespace WebThreads.Services
{
	public class ThreadsResizer
	{
		private readonly ImageService _imageService;
		private int _count;
		private List<string> _processedFiles;
		private static ThreadsResizer instance;
		public static ThreadsResizer Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ThreadsResizer();
				}
				return instance;
			}
		}

		public ThreadsResizer()
		{
			_imageService = new ImageService();
			_processedFiles = new List<string>();
        }

		public void StopResizing()
		{
			Interlocked.Exchange(ref _count, 0);
		}

		public List<string> GetProcessedFiles()
		{
			return _processedFiles;
		}

		public void ThreadResize(Options options)
		{
			_processedFiles = new List<string>();
			string[] Extensions = new string[] { "*.jpg", "*.jpeg", "*.png", "*.gif" };
			string[] ImageSizes = options.Sizes.Split(';');
			ThreadPool.SetMaxThreads(options.ThreadsNumber, options.ThreadsNumber);

			if (!Directory.Exists(options.OutputFolder))
			{
				Directory.CreateDirectory(options.OutputFolder);
			}

			string[] paths = _imageService.FindImages(options.InputFolder, Extensions);
			_count = paths.Length;
			ManualResetEvent resizeFinished = new ManualResetEvent(false);

			foreach (var path in paths)
			{
				ThreadPool.QueueUserWorkItem((o) => {
					string FileName = Path.GetFileName(path);
					string imagePath = Path.Combine(options.InputFolder, FileName);

					foreach (var size in ImageSizes)
					{
						_imageService.SaveImg(_imageService.ResizeImage(imagePath, size), Path.Combine(options.OutputFolder, size + "_" + FileName));
					}

					_processedFiles.Add(FileName);

					if (Interlocked.Decrement(ref _count) <= 0)
					{
						resizeFinished.Set();
					}

				});
			}
			WaitHandle.WaitAll(new ManualResetEvent[] { resizeFinished });
		}
	}
}