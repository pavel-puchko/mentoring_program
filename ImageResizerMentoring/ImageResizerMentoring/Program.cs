using System;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;


namespace ImageResizerMentoring
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			new Program();
		}

		Program()
		{
			set_params();

			ThreadPool.SetMaxThreads(ApplicationParams.threadsCount, ApplicationParams.threadsCount);

			if (!Directory.Exists(ApplicationParams.outputFolder))
			{
				Directory.CreateDirectory(ApplicationParams.outputFolder);
			}


			string[] paths = find_images(ApplicationParams.inputFolder, new string[] { "*.jpg", "*.jpeg", "*.png", "*.gif" });
			int count = paths.Length;

			foreach (var path in paths)
			{
				ThreadPool.QueueUserWorkItem((o) => {
					string fileName = Path.GetFileName(path);
                    Image imgFromPath = get_image(fileName);
					foreach (var size in ApplicationParams.imageSizes)
					{
						Image resizedImage = resize_image(imgFromPath, size);
						save_img(resizedImage, Path.Combine(ApplicationParams.outputFolder, size + "_" + fileName ));
					}
			

					if (Interlocked.Decrement(ref count) == 0)
					{
						resizeFinished.Set();
					}

					GC.Collect();
					GC.WaitForPendingFinalizers();
				});
			}
			WaitHandle.WaitAll(new ManualResetEvent[] { resizeFinished });
		}

		ManualResetEvent resizeFinished = new ManualResetEvent(false);

		void set_params()
		{
			ApplicationParams.inputFolder = ConfigurationManager.AppSettings["inputFolder"];
			ApplicationParams.outputFolder = ConfigurationManager.AppSettings["outputFolder"];
			ApplicationParams.threadsCount = int.Parse(ConfigurationManager.AppSettings["threadsCount"]);
			ApplicationParams.threadImagesCount = int.Parse(ConfigurationManager.AppSettings["threadImagesCount"]);
			ApplicationParams.imageSizes = ConfigurationManager.AppSettings["imageSizes"].Split(';');
		}

		String[] find_images(String path, String[] patterns)
		{
			var list = new List<string>();

			foreach (var pattern in patterns)
			{
				foreach (var p in Directory.GetFiles(path, pattern, SearchOption.AllDirectories))
				{
					list.Add(p);
				}
			}

			return list.ToArray();
		}

		Image get_image(string path)
		{
			using (FileStream stream = new FileStream(
				Path.Combine(ApplicationParams.inputFolder, path), FileMode.Open, FileAccess.Read)
			)
			{
				Image img = Image.FromStream(stream);

				return img;
			}
		}

		Image resize_image(Image img, string size)
		{
			var sizeXY = size.Split('x');
			var new_img = new Bitmap(int.Parse(sizeXY[0]), int.Parse(sizeXY[1]));
			Graphics.FromImage(new_img).DrawImage(img, 0, 0, new_img.Width, new_img.Height);

			return new_img;
		}

		void save_img(Image img, String save_to)
		{
			img.Save(save_to);
		}
	}
}
