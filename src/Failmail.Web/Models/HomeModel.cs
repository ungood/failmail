using System;
using System.Collections.Generic;
using System.Linq;
using Failmail.Core.Model;

namespace Failmail.Web.Models
{
    public class HomeModel
    {
        public IList<TagCloud> TagClouds { get; set; } 
    }
}