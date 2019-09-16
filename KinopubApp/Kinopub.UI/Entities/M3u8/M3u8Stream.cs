﻿using System;
using System.Collections.Generic;
using Windows.Graphics.Display;

namespace Kinopub.UI.Entities.M3u8
{
    public class M3u8Stream
    {
        public string ProgramId { get; set; }
        public long Bandwidth { get; set; }
        public int Resolution { get; set; }
        public List<M3u8Audio> AudioTrack { get; set; }
        public List<M3u8Subtitle> SubtitleTrack { get; set; }
        public Uri Uri { get; set; }
    }
}