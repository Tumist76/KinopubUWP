using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Kinopub.UI.Entities.M3u8
{
    public class M3u8Subtitle
    {
        public string Name { get; set; }
        public string GroupId { get; set; }

        public string Language
        {
            get
            {
                return Name.Substring(0,3);
            }
        }

        public bool IsForced
        {
            get
            {
                if (Name.Contains("Forced")) return true;
                else return false;
            }
        }

        public Uri Uri { get; set; }

    }
}
