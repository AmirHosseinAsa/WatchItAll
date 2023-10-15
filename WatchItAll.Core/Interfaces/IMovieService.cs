using System.Collections.Generic;
using System.Threading.Tasks;
using WatchItAll.DataLayer.Entities;

namespace WatchItAll.Core.Interfaces
{
    public interface IMovieService
    {
        #region Response
        Task<string> GetPageHtmlAsync(string route, bool dontUseBaseUrl = false);
        #endregion

        #region Show
        Task<HomePageData> GetHomeItemsAsync(string url);
        Task<(MovieDetail mv, string imdbRate)> GetMovieDetailAsync(string url);
        Task<List<MovieItem>> GetMovieListItemAsync(string responce);
        Task<List<ServerItem>> GetServerIdsAsync(string episodeId);
        Task<List<EpisodeItem>> GetEpisodesOfASeasonAsync(string seasonNumber,string seasonId, string url = "");
        #endregion

        #region Storage
        Task SaveMVSAsync(MovieItem movieItem, string type = "");
        Task SavePlayedAsync(EpisodeItemAndServersMovieItem episodeOrServerItem);
        Task DeletePlayedAsync(string KEY);
        Task DeleteMVSAsync(MovieItem movieItem, string type = "");
        Task<bool> IsMVSBookmarkedAsync(MovieItem movieItem, string type = "");
        Task<List<MovieItem>> LoadMVSAsync(string type = "");
        Task SaveSearchAsync(string search);
        Task<List<Search>> LoadSavedSearchAsync();
        Task DeleteSearchAsync(string search);
        Task<List<EpisodeItemAndServersMovieItem>> GetRecentPlayedAsync();
        #endregion
    }
}
