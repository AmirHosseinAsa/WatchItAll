using System.Collections.Generic;

namespace WatchItAll.DataLayer.Entities
{
    public class MovieDetail
    {
        public string TrailerLink{ get; set; }
        public string Long{ get; set; }
        public string Overview{ get; set; }
        public string Released{ get; set; }
        public List<ItemCastORGenre> Genres{ get; set; }
        public List<ItemCastORGenre> Casts{ get; set; }
        public List<SeasonItem> Seasons { get; set; }
        public List<StreamURlForMovieItem> StreamURlsForMovie{ get; set; }
        public string PosterBackgroundURl{ get; set; }

    }
}
