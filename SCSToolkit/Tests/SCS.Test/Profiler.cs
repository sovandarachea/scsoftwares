using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using SCS.Test.EFM;

namespace SCS.Test
{
    public class Profiler
    {
        [Test]
        public void test()
        {
            //var mf = GetMediaFiles("http://nothing.new/entry/86");
            //var mfString = JsonConvert.SerializeObject(mf);
            //Console.WriteLine(mfString);
            var nums = Enumerable.Range(1, 400).ToList();
            Parallel.ForEach(nums, new ParallelOptions { MaxDegreeOfParallelism = 10 }, (num =>
            {
                var url = $"http://nothing.new/entry/{num}";
                var fl = GetFileLink(url);
                Console.WriteLine($"{fl == null} - {url}");
            }));
        }

        [Test]
        public void Index()
        {
            var fls = new List<FileLink>();
            using (var ae = new A51())
            {
                fls = ae.FileLinks.ToList();
            }
            Parallel.ForEach(fls, new ParallelOptions { MaxDegreeOfParallelism = 10 }, (fl =>
            {
                if (string.IsNullOrWhiteSpace(fl?.UrlsJson) == false)
                {
                    var urls = JsonConvert.DeserializeObject<List<string>>(fl.UrlsJson);
                    using (var ae = new A51())
                    {
                        urls.ForEach(u =>
                        {
                            var f = new FileLoader
                            {
                                Url = u,
                                UpdateOn = DateTimeOffset.Now                                
                            };
                            var exist = ae.FileLoaders.FirstOrDefault(a => a.Url == u);
                            f.Id = exist?.Id ?? Guid.NewGuid();
                            ae.FileLoaders.AddOrUpdate(f);
                            ae.SaveChanges();
                        });
                    }
                }
            }));
        }

        [TestCase("http://nothing.new/entry/381")]
        [TestCase("http://nothing.new/entry/371")]
        public void GetMediaFilesTest(string url)
        {
            var mfs = GetFileLink(url);
            var mfString = JsonConvert.SerializeObject(mfs);
            Console.WriteLine(mfString);
            Assert.IsNotNull(mfs);
        }

        public static FileLink GetFileLink(string url)
        {
            var urls = GetMediaUrls(url);
            if (urls == null || urls.Count == 0) return null;
            var fl = new FileLink
            {
                BaseUrl = url,
                UpdateOn = DateTimeOffset.Now,
                UrlsJson = JsonConvert.SerializeObject(urls)
            };
            using (var ae = new A51())
            {
                var exist = ae.FileLinks.FirstOrDefault(a => a.BaseUrl == fl.BaseUrl);
                fl.Id = exist?.Id ?? Guid.NewGuid();
                ae.FileLinks.AddOrUpdate(fl);
                ae.SaveChanges();
            }
            return fl;
        }
        public static List<string> GetMediaUrls(string url)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            var htmlContent = "";
            try
            {
                htmlContent = httpClient.GetStringAsync("").Result;
            }
            catch (AggregateException)
            {
            }
            var urls = new List<string>();
            var imgVidParser = new Regex(@"(['\""])(/?(images|video)[^'\""]*)");
            var matches = imgVidParser.Matches(htmlContent);
            foreach (Match m in matches)
            {
                urls.Add(m.Groups[2].Value);
            }
            if (urls?.Count > 0)
            {
                urls = urls.Distinct().ToList();
            }
            return urls;
        }
        public static MediaFile GetMediaFiles(string url)
        {
            var imgParser = new Regex(@"<img[^>]*>");
            var srcParser = new Regex(@"(.*src=['\""])([^'\""]*)(.*)");
            var vidParser = new Regex(@"(.*flashvars=\"")([^'\""]*)");
            var mf = new MediaFile {Url = new List<string>()};
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            try
            {
                var task = httpClient.GetStringAsync("").Result;
                var matches = imgParser.Matches(task);
                foreach (Match m in matches)
                {
                    var src = srcParser.Match(m.Value);
                    var img = src.Groups[2];
                    mf.Url.Add(img.Value);
                }
                Match mv = vidParser.Match(task);
                if (mv.Success)
                {
                    mf.Url.Add(mv.Groups[2].Value);
                }
                if (mf?.Url?.Count > 0)
                {
                    mf.Url = mf.Url.Distinct().ToList();
                }
                return mf;
            }
            catch (AggregateException)
            {
                return null;
            }
        }
        [Test]
        public void Downloads()
        {
            var fls = new List<FileLoader>();
            using (var ae = new A51())
            {
                fls = ae.FileLoaders.ToList();
            }
            //Parallel.ForEach(fls, new ParallelOptions { MaxDegreeOfParallelism = 10 }, (fl =>
            fls.ForEach(fl =>
            {
                var url = $"http://nothing.new{fl.Url}";
                Download(url);
            }
                );
            //));
        }

        [Test]
        public void DownloadAll()
        {
            var basePath = @"E:\WD Softwares\imgurs\amy\";
            var urlList =
                (from i in Enumerable.Range(1, 30) //{i:00}
                 select new { i = i, url = $"dd" }).ToList();
            Parallel.ForEach(urlList, new ParallelOptions {MaxDegreeOfParallelism = 20}, (url =>
            {
                Download($"{url.url}", basePath, $"2ih_");
            }));
        }
        public static void Download(string url, string basePath = @"C:\GIT\Personals\SCS\scsoftwares\SCSToolkit\Tests\SCS.Test\aImgs\", string fNamePrefix = "")
        {
            url = url.Replace(".thumb", "");
            var fNameParser = new Regex("(.*/)(.*)");
            var fName = fNameParser.Match(url).Groups[2].Value;
            var fPath = $"{basePath}{fNamePrefix}{fName}";
            if (File.Exists(fPath))
            {
                return;
            }
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            try
            {
                var file = httpClient.GetByteArrayAsync("").Result;
                File.WriteAllBytes(fPath, file);
            }
            catch (Exception) { }
        }
    }

    public class MediaFile
    {
        public List<string> Url { get; set; }
    }
}
