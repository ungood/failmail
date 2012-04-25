using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Failmail.Core.Model
{
    public class Image
    {
        public Guid Key { get; set; }
        public IList<string> Tags { get; set; }
    }
}
