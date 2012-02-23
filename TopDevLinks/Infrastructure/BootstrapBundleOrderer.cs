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
            if (files.Count() > 2)
                throw new NotSupportedException("I can only handle two files in this bundle: bootstrap.css and bootstrap-responsive.css");

            var bootstrap = files.FirstOrDefault(f => f.Name == "bootstrap.css");
            var bootstrapResponsive = files.First(f => f.Name == "bootstrap-responsive.css");

            if (bootstrap == null)
                throw new NullReferenceException("Can't find bootstrap.css.");
            if (bootstrapResponsive == null)
                throw new NullReferenceException("Can't find bootstrap-responsive.css");

            return new List<FileInfo>() { bootstrap, bootstrapResponsive };
        }
    }
}