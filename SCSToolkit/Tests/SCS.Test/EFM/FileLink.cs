using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Test.EFM
{
    public class FileLink
    {
        public Guid Id { get; set; }
        public string BaseUrl { get; set; }
        public string UrlsJson { get; set; }
        public DateTimeOffset? UpdateOn { get; set; }
    }
}
