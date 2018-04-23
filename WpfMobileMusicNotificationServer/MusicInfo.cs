using System;

namespace WpfMobileMusicNotificationServer
{
    public class MusicInfo
    {
        public int TrackNumber { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Song { get; set; }

        public override string ToString() => $"{TrackNumber}. {Song}{Environment.NewLine}{Album}{Environment.NewLine}{Artist}";
    }
}