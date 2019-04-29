using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Infra.Files.Image
{
    public class ImageLinkCache
    {
        public Dictionary<string, Uri> Cache; // cache com os links

        public ImageLinkCache()
        {
            Cache = new Dictionary<string, Uri>();
        }
        
    }
}
