using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.IO;

namespace TopDevLinks.Infrastructure
{
    public class BootstrapBundleOrderer : IBundleOrderer
    {
        public IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
        {
            return new List<FileInfo>()
            {
                files.First(f => f.Name == "bootstrap.css"),
                files.First(f => f.Name == "bootstrap-responsive.css")
            };
        }
    }
}