using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMobileMusicNotificationServer
{
    public class MusicInfo
    {
        public int TrackNumber { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Song { get; set; }

        public override string ToString()
        {
            return $"{TrackNumber}. {Song}{Environment.NewLine}{Album}{Environment.NewLine}{Artist}";
        }
    }
}
