using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Test.EFM
{
    public class FileLoader
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public byte[] Contents { get; set; }
        public DateTimeOffset? UpdateOn { get; set; }
    }
}
