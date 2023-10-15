namespace WatchItAll.DataLayer.Entities
{
    public class EpisodeItemAndServersMovieItem
    {
        public EpisodeItem SelectedEpisode { get; set; }
        public StreamURlForMovieItem MovieStreamURls { get; set; }
        public MovieItem MovieItem { get; set; }
        public string KEY { get; set; }

        public EpisodeItemAndServersMovieItem(EpisodeItem episde, StreamURlForMovieItem streamURls, MovieItem movieItem) {
            this.SelectedEpisode = episde;
            this.MovieStreamURls = streamURls;
            this.MovieItem = movieItem;
        }
    }
}
