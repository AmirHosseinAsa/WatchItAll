using System.Collections.Generic;

namespace WatchItAll.DataLayer.Entities
{
    public class HomePageData
    {
        public List<MovieItem> TrendingMovieItems { get; set; }
        public List<MovieItem> TrendingTvItems { get; set; }
        public List<MovieItem> LatestMoviesItems { get; set; }
        public List<MovieItem> LatestTvShowsItems { get; set; }

        public HomePageData(List<MovieItem> trendingMovieItems, List<MovieItem> trendingTvItems, List<MovieItem> latestMoviesItems, List<MovieItem> latestTvShowsItems)
        {
            this.TrendingMovieItems = trendingMovieItems ?? null;
            this.TrendingTvItems = trendingTvItems ?? null;
            this.LatestMoviesItems = latestMoviesItems;
            this.LatestTvShowsItems = latestTvShowsItems;
        }
    }
}
