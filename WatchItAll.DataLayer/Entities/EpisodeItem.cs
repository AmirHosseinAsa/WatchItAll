namespace WatchItAll.DataLayer.Entities
{
    public class EpisodeItem
    {
        public string Title { get; set; }
        public string ServerId{ get; set; }
        public string Thumbnail { get; set; }
        public string EpisodeNumber { get; set; }
        public string MovieURl { get; set; }
        public string SeasonNumber { get; set; }

        public EpisodeItem(string title, string serverId, string thumbnail, string episodeNumber,string movieURl, string season)
        {
            this.Title = title;
            this.Thumbnail = thumbnail;
            this.ServerId = serverId;
            this.EpisodeNumber = episodeNumber;
            this.MovieURl = movieURl;
            SeasonNumber = season;
        }
    }
}
