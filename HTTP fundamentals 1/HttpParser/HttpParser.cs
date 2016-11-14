using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;

namespace HttpParser
{
    public class HttpParser
    {
        public void ParseUrl(string url, string directory, int level ,ImageFormat format)
        {
            Console.WriteLine("Start");
            Console.ForegroundColor=ConsoleColor.Yellow;
            List<string> pageLinks = new List<string>();
            Dictionary<int, List<string>> pages = new Dictionary<int,List<string>>();

            string html = GetRequest(url).Result;
            Console.WriteLine(html);
            var angle = new HtmlParser().Parse(html);

            foreach (var element in angle.QuerySelectorAll("a"))
            {
                string adress = element.GetAttribute("href");
                if (adress.StartsWith("//"))
                {
                    var newLink = adress.Replace("//", "http://");
                    pageLinks.Add(newLink);
                }
            }
            pages.Add(0,pageLinks);

            int i = 1;

            for (int j = 0; j < level; j++)
            {
                AddLinks(pages[j], pages, j+1);
            }

            foreach (var item in pages.Values)
            {
                foreach (var it in item)
                {
                    DownloadImages(it, directory, format);
                }
            }     
            Console.WriteLine("Finished");
        }

        public void AddLinks(List<string> pageLinks, Dictionary<int,List<string>> pages, int i)
        {
            var xx = new List<string>();
            foreach (var link in pageLinks)
            {
                string html1 = GetRequest(link).Result;
                Console.WriteLine(html1);
                var angl = new HtmlParser().Parse(html1);

                foreach (var element in angl.QuerySelectorAll("a"))
                {
                    string adress = element.GetAttribute("href");
                    if (adress != null)
                    {
                        if (adress.StartsWith("//"))
                        {
                            var newLink = adress.Replace("//", "http://");
                            xx.Add(newLink);
                        }
                    }
                }
            }
            if (xx.Count > 0)
            {
                pages.Add(i, xx);
                i++;
            }            
        }

        public void DownloadImages(string path,string directory, ImageFormat format)
        {
            string html = GetRequest(path).Result;
            Console.WriteLine(html);
            var angle = new HtmlParser().Parse(html);

            foreach (var image in angle.QuerySelectorAll("img"))
            {
                string url1 = image.GetAttribute("src");
                url1 = url1.Replace("//", "http://");
                if (url1.Contains("ht")==false)
                {
                    url1 = path + url1.TrimStart('/');
                }
                using (WebClient client = new WebClient())
                {
                    try
                    {
                      byte[] data = client.DownloadDataTaskAsync(new Uri(url1)).Result;
                      using (MemoryStream mem = new MemoryStream(data))
                      {
                        using (var newImage = Image.FromStream(mem))
                        {
                            Random rnd = new Random();
                            var randomName = rnd.Next(10, 9999);
                            newImage.Save(directory + randomName + ".png", format);
                        }
                      }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);                     
                    }
                    Console.WriteLine(url1);
                }
            }
        }

        public async Task<string> GetRequest(string url)
        {
            string myContent= "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(url))
                    {
                        using (var content = response.Content)
                        {
                            myContent = await content.ReadAsStringAsync();
                        }
                    }
                }
                catch (Exception e)
                {
                    myContent = e.Message;
                }
            }
            return myContent;
        }
    }
}
