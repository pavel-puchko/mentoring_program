using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

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

		public Image<Bgr, byte> ResizeImage(string path, string size)
		{
			var sizeXY = size.Split('x');
			var width = int.Parse(sizeXY[0]);
			var height = int.Parse(sizeXY[1]);
            var image = new Image<Bgr, byte>(path);
			var resizedImage = image.Resize(width, height, Inter.Cubic);

			return resizedImage;
		}

		public void SaveImg(Image<Bgr, byte> img, string saveToPath)
		{
			img.Save(saveToPath);
		}
	}
}