using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinopub.UI.Entities.M3u8
{
    public class M3u8Audio
    {
        public string Name { get; set; }
        public string GroupId { get; set; }

        public string Language
        {
            get { return Name.Substring(Name.Length - 5).Trim('(', ')'); }
        }
        public bool IsDefaultTrack { get; set; }
        public Uri Uri { get; set; }
    }
}
