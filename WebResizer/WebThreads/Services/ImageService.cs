using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WebThreads.Services
{
	public class ImageService
	{
		public string[] FindImages(string path, string[] patterns)
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
		public Image GetImage(string path, string folder)
		{
			using (FileStream stream = new FileStream(
				Path.Combine(folder, path), FileMode.Open, FileAccess.Read)
			)
			{
				Image img = Image.FromStream(stream);

				return img;
			}
		}

		public Image ResizeImage(Image img, string size)
		{
			var sizeXY = size.Split('x');
			var new_img = new Bitmap(int.Parse(sizeXY[0]), int.Parse(sizeXY[1]));
			Graphics.FromImage(new_img).DrawImage(img, 0, 0, new_img.Width, new_img.Height);

			return new_img;
		}

		public void SaveImg(Image img, string saveTo)
		{
			img.Save(saveTo);
		}
	}
}