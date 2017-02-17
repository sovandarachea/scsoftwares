using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SCSToolkit.Controllers
{
    public class ExperimentsController : Controller
    {
        // GET: Experiments
        public ActionResult Index()
        {
            return View();
        }

        public bool IsValidPage(string url)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            var msg = httpClient.GetAsync("").Result;
            return msg.IsSuccessStatusCode;
        }
        public string GetHtml(string url, bool noBaseReplace = false, string baseUrl = "")
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            var html = httpClient.GetStringAsync("").Result;
            if (!noBaseReplace && !string.IsNullOrWhiteSpace(html))
            {
                var uri = new Uri(url);
                var baseUri = uri.GetLeftPart(UriPartial.Authority);
                var imgUrl = "/Experiments/GetFile?url=" + baseUri;
                html = 
                    html.Replace("href=\"/", $"href=\"{baseUri}/")
                    .Replace("src=\"/", $"src=\"{imgUrl}/");
            }
            return html;
        }

        public ActionResult GetFile(string url)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            var file = httpClient.GetByteArrayAsync("").Result;
            return File(file, "image");
        }
    }
}