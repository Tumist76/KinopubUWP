using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinopub.UI.Enities.M3u8
{
    class Subtitle
    {
        public string Name { get; set; }
        public string GroupId { get; set; }
        public string Language { get; set; }
        public bool IsForced { get; set; }
        public Uri Uri { get; set; }

    }
}
