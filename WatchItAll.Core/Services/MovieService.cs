using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using WatchItAll.Core.Interfaces;
using WatchItAll.DataLayer.Constants;
using WatchItAll.DataLayer.Entities;
using Windows.Storage;

namespace WatchItAll.Core.Services
{
    public class MovieService : IMovieService
    {
        public async Task<HomePageData> GetHomeItemsAsync(string responce)
        {
            List<MovieItem> TrendingMovieItems = new List<MovieItem>();
            List<MovieItem> TrendingTvItems = new List<MovieItem>();
            List<MovieItem> LatestMoviesItems = new List<MovieItem>();
            List<MovieItem> LatestTvShowItems = new List<MovieItem>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responce);

            #region Trending Movies
            HtmlNode trendingMoviesNode = doc.DocumentNode.SelectSingleNode("//*[@id='trending-movies']");

            if (trendingMoviesNode != null)
            {
                var flwItems = trendingMoviesNode.SelectNodes(".//*[contains(@class, 'flw-item')]");

                if (flwItems != null)
                {
                    foreach (var flwItem in flwItems)
                    {
                        TrendingMovieItems.Add(await ExtractMovieItemFromItem(flwItem));
                    }
                }
            }
            #endregion

            #region Trending TV
            HtmlNode trendingTvNode = doc.DocumentNode.SelectSingleNode("//*[@id='trending-tv']");

            if (trendingTvNode != null)
            {
                var flwItems = trendingTvNode.SelectNodes(".//*[contains(@class, 'flw-item')]");

                if (flwItems != null)
                {
                    foreach (var flwItem in flwItems)
                    {
                        TrendingTvItems.Add(await ExtractMovieItemFromItem(flwItem));
                    }
                }
            }
            #endregion

            #region Trending Movies
            HtmlNode latestMoviesNode = doc.DocumentNode.SelectNodes(".//*[contains(@class, 'section-id-02')]").FirstOrDefault();

            if (latestMoviesNode != null)
            {
                var flwItems = latestMoviesNode.SelectNodes(".//*[contains(@class, 'flw-item')]");

                if (flwItems != null)
                {
                    foreach (var flwItem in flwItems)
                    {
                        LatestMoviesItems.Add(await ExtractMovieItemFromItem(flwItem));
                    }
                }
            }
            #endregion

            #region Trending TV
            HtmlNode latestTvShowsNode = doc.DocumentNode.SelectNodes(".//*[contains(@class, 'section-id-02')]")?[1];

            if (latestTvShowsNode != null)
            {
                var flwItems = latestTvShowsNode.SelectNodes(".//*[contains(@class, 'flw-item')]");

                if (flwItems != null)
                {
                    foreach (var flwItem in flwItems)
                    {
                        LatestTvShowItems.Add(await ExtractMovieItemFromItem(flwItem));
                    }
                }
            }
            #endregion


            return new HomePageData(TrendingMovieItems, TrendingTvItems, LatestMoviesItems, LatestTvShowItems);
        }

        async Task<MovieItem> ExtractMovieItemFromItem(HtmlNode item)
        {
            var imgSrc = item.SelectSingleNode(".//img")?.GetAttributeValue("data-src", "");
            var fdiItemText = item.SelectNodes(".//*[contains(@class, 'fd-infor')]/span").FirstOrDefault().InnerText;
            var year = HttpUtility.HtmlDecode(item.SelectNodes(".//*[contains(@class, 'fd-infor')]/span").LastOrDefault().InnerText);
            var filmNameNode = item.SelectSingleNode(".//*[contains(@class, 'film-name')]/a");
            var filmNameText = HttpUtility.HtmlDecode(filmNameNode?.InnerText);
            var href = filmNameNode?.GetAttributeValue("href", "");
            return new MovieItem(filmNameText, imgSrc, "", href, fdiItemText, year);
        }

        public async Task<string> GetPageHtmlAsync(string route, bool dontUseBaseUrl = false)
        {
            var html = "";
            try
            {
                string url = "";
                if (!dontUseBaseUrl)
                    url += Staticants.baseUrl;
                url += route;

                using (HttpClient _client = new HttpClient())
                {
                    var response = await _client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    html = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
            }
            return html;
        }

        public async Task<(MovieDetail mv, string imdbRate)> GetMovieDetailAsync(string url)
        {
            var mvDet = new MovieDetail();
            var responce = await GetPageHtmlAsync(url);
            var movieId = url.Split("-").LastOrDefault();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responce);

            if (url.ToLower().Contains("/tv/"))
            {
                mvDet.Seasons = new List<SeasonItem>();

                var varResponceSeasons = await GetPageHtmlAsync("ajax/season/list/" + movieId);
                var docSeasons = new HtmlDocument();
                docSeasons.LoadHtml(varResponceSeasons);
                var aTags = docSeasons.DocumentNode.SelectNodes(".//*[contains(@class, 'dropdown-menu')]/a");
                foreach (var aTag in aTags)
                {
                    string dataId = aTag.GetAttributeValue("data-id", "");
                    string text = aTag.InnerText.Replace("\n", "").Trim();
                    mvDet.Seasons.Add(new SeasonItem(dataId, text, null));
                }

                if (mvDet.Seasons.Any())
                {
                    mvDet.Seasons.FirstOrDefault().Episodes = await GetEpisodesOfASeasonAsync(mvDet.Seasons.FirstOrDefault().Title, mvDet.Seasons.FirstOrDefault().Id, url);
                }
            }
            else
            {
                mvDet.StreamURlsForMovie = new List<StreamURlForMovieItem>();
                var varResponceMovie = await GetPageHtmlAsync("ajax/episode/list/" + movieId);

                var docMovie = new HtmlDocument();
                docMovie.LoadHtml(varResponceMovie);
                var aTags = docMovie.DocumentNode.SelectNodes(".//a");

                for (int i = 0; i < aTags.Count(); i++)
                {
                    string dataId = aTags[i].GetAttributeValue("data-id", "");
                    mvDet.StreamURlsForMovie.Add(new StreamURlForMovieItem($"Play From Server {i + 1}", dataId));
                }
            }


            #region Poster
            HtmlNode ogImageNode = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
            if (ogImageNode != null)
                mvDet.PosterBackgroundURl = ogImageNode.GetAttributeValue("content", "");
            #endregion

            #region Casts And Genres
            HtmlNodeCollection divNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'row-line')]");
            foreach (HtmlNode divNode in divNodes)
            {
                if (divNode.InnerHtml.Contains("Release"))
                {
                    Regex regex2 = new Regex(@"\d{4}-\d{2}-\d{2}");
                    Match match2 = regex2.Match(divNode.InnerText);
                    if (match2.Success)
                        mvDet.Released = match2.Value;
                }
                else
                if (divNode.InnerHtml.Contains("Genre"))
                {
                    var aTags = divNode.SelectNodes(".//a");
                    if (aTags != null)
                    {
                        mvDet.Genres = new List<ItemCastORGenre>();
                        foreach (var aTag in aTags)
                        {
                            string href = aTag.GetAttributeValue("href", "");
                            string text = HttpUtility.HtmlDecode(aTag.InnerText);
                            mvDet.Genres.Add(new ItemCastORGenre(text, href));
                        }
                    }
                }
                else
                if (divNode.InnerHtml.Contains("Casts"))
                {
                    var aTags = divNode.SelectNodes(".//a");
                    if (aTags != null)
                    {
                        mvDet.Casts = new List<ItemCastORGenre>();
                        foreach (var aTag in aTags)
                        {
                            string href = aTag.GetAttributeValue("href", "");
                            string text = aTag.InnerText;
                            mvDet.Casts.Add(new ItemCastORGenre(text, href));
                        }
                    }
                }
            }
            #endregion

            #region Overview
            HtmlNode overViewNode = doc.DocumentNode.SelectSingleNode("//*[@class='description']");
            if (overViewNode != null)
                mvDet.Overview = overViewNode.InnerText.Replace("Overview:", "").Trim();
            #endregion

            #region trailer
            HtmlNode iframeNode = doc.DocumentNode.SelectSingleNode("//*[@id='iframe-trailer']");
            if (iframeNode != null)
                mvDet.TrailerLink = iframeNode.GetAttributeValue("data-src", "");
            #endregion

            #region IMDB

            //class="imdb"

            string imdbRate = doc.DocumentNode.SelectSingleNode(".//*[contains(@class, 'imdb')]").InnerText.Split(":").LastOrDefault();

            #endregion

            return (mvDet, imdbRate);
        }

        public async Task<List<MovieItem>> GetMovieListItemAsync(string responce)
        {
            var result = new List<MovieItem>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responce);

            var flwItems = doc.DocumentNode.SelectNodes(".//*[contains(@class, 'flw-item')]");

            if (flwItems != null)
            {
                foreach (var flwItem in flwItems)
                {
                    result.Add(await ExtractMovieItemFromItem(flwItem));
                }
            }
            return result;
        }

        public async Task<List<ServerItem>> GetServerIdsAsync(string episodeId)
        {
            List<ServerItem> servers = new List<ServerItem>();

            using (HttpClient _client = new HttpClient())
            {
                HttpResponseMessage response = await _client.GetAsync(Staticants.baseUrl + "ajax/episode/servers/" + episodeId);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(responseBody);
                var aTags = doc.DocumentNode.SelectNodes("//a");
                foreach (var aTag in aTags)
                {
                    string id = aTag.GetAttributeValue("id", "");
                    servers.Add(new ServerItem("", id.Split("-").LastOrDefault()));
                }
            }

            return servers;
        }

        public async Task<List<EpisodeItem>> GetEpisodesOfASeasonAsync(string seasonNumber, string seasonId, string url = "")
        {
            List<EpisodeItem> episodes = new List<EpisodeItem>();

            var varResponceEpisodes = await GetPageHtmlAsync("ajax/season/episodes/" + seasonId);
            var docEpisodes = new HtmlDocument();
            docEpisodes.LoadHtml(varResponceEpisodes);
            var epItems = docEpisodes.DocumentNode.SelectNodes(".//*[contains(@class, 'eps-item')]");
            foreach (var item in epItems)
            {
                var imgSrc = item.Descendants("img").FirstOrDefault()?.GetAttributeValue("src", "");
                if (!imgSrc.Contains("https"))
                    imgSrc = "/Images/ep-no-thumb.jpg";

                var episodeNumber = HttpUtility.HtmlDecode(item.SelectSingleNode(".//*[contains(@class, 'episode-number')]")?.InnerText).Replace("\n", "").Replace(":", "").Trim();
                var lastATagText = HttpUtility.HtmlDecode(item.Descendants("a").LastOrDefault()?.InnerText);
                var serverId = item.GetAttributeValue("data-id", "");
                episodes.Add(new EpisodeItem(lastATagText, serverId, imgSrc, episodeNumber, url, seasonNumber));
            }

            return episodes;
        }

        public async Task SaveMVSAsync(MovieItem movieItem, string type = "")
        {
            var existingMovies = await LoadMVSAsync(type);
            if (existingMovies.Any())
            {
                existingMovies.RemoveAll(a => a.Url == movieItem.Url);
            }
            else
            {
                existingMovies = new List<MovieItem>();
                movieItem.DateAdded = DateTime.Now;
            }

            existingMovies.Add(movieItem);

            var json = JsonConvert.SerializeObject(existingMovies);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(type, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);
        }

        public async Task DeleteMVSAsync(MovieItem movieItem, string type = "")
        {
            var existingMovies = await LoadMVSAsync(type);
            if (existingMovies != null)
            {
                existingMovies.RemoveAll(a => a.Url == movieItem.Url);
                var json = JsonConvert.SerializeObject(existingMovies);
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(type, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, json);
            }
        }

        public async Task<bool> IsMVSBookmarkedAsync(MovieItem movieItem, string type = "")
        {
            var existingMovies = await LoadMVSAsync(type);
            if (existingMovies != null)
                return existingMovies.Any(a => a.Url == movieItem.Url);
            return false;
        }

        public async Task<List<MovieItem>> LoadMVSAsync(string type = "")
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync(type);
                var json = await FileIO.ReadTextAsync(file);
                var animes = JsonConvert.DeserializeObject<List<MovieItem>>(json);
                if (animes == null)
                {
                    return new List<MovieItem>();
                }
                else
                {
                    var orderedMovies = animes.OrderByDescending(a => a.DateAdded).ToList();
                    return orderedMovies;
                }
            }
            catch (FileNotFoundException)
            {
                return new List<MovieItem>();
            }
        }

        public async Task SaveSearchAsync(string search)
        {
            var existingSearches = await LoadSavedSearchAsync();
            if (existingSearches != null)
            {
                // Remove the old anime if it exists
                existingSearches.RemoveAll(a => a.Text.ToLower() == search.ToLower());
            }

            // Add the new anime
            existingSearches.Add(new Search(search, DateTime.Now));

            var json = JsonConvert.SerializeObject(existingSearches);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("searches.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);
        }

        public async Task<List<Search>> LoadSavedSearchAsync()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("searches.json");
                var json = await FileIO.ReadTextAsync(file);
                var searches = JsonConvert.DeserializeObject<List<Search>>(json);
                if (searches == null)
                {
                    return new List<Search>();
                }
                else
                {
                    var orderedSearches = searches.OrderByDescending(a => a.Date).ToList();
                    return orderedSearches;
                }
            }
            catch (FileNotFoundException)
            {
                return new List<Search>();
            }
        }

        public async Task DeleteSearchAsync(string search)
        {
            var existingSearches = await LoadSavedSearchAsync();
            if (existingSearches != null)
            {
                existingSearches.RemoveAll(a => a.Text.ToLower() == search.ToLower());
                var json = JsonConvert.SerializeObject(existingSearches);
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("searches.json", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, json);
            }
        }

        public async Task SavePlayedAsync(EpisodeItemAndServersMovieItem episodeOrServerItem)
        {
            List<EpisodeItemAndServersMovieItem> epOrMvPlayed = await GetRecentPlayedAsync();
            if (epOrMvPlayed.Any())
            {
                if (episodeOrServerItem.SelectedEpisode != null)
                {
                    try
                    {
                        epOrMvPlayed.Where(a => a.SelectedEpisode != null).ToList().RemoveAll(a => a.SelectedEpisode.EpisodeNumber == episodeOrServerItem.SelectedEpisode.EpisodeNumber && a.SelectedEpisode.Title == episodeOrServerItem.SelectedEpisode.Title);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    epOrMvPlayed.Where(a => a.SelectedEpisode == null).ToList().RemoveAll(a => a.MovieItem.Url == episodeOrServerItem.MovieItem.Url);
                }
            }
            else
            {
                epOrMvPlayed = new List<EpisodeItemAndServersMovieItem>();
                episodeOrServerItem.MovieItem.DateAdded = DateTime.Now;
            }
            episodeOrServerItem.KEY = Guid.NewGuid().ToString();
            epOrMvPlayed.Add(episodeOrServerItem);

            var json = JsonConvert.SerializeObject(epOrMvPlayed);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("recently_played_mvs.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);
        }

        public async Task<List<EpisodeItemAndServersMovieItem>> GetRecentPlayedAsync()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("recently_played_mvs.json");
                var json = await FileIO.ReadTextAsync(file);
                var animes = JsonConvert.DeserializeObject<List<EpisodeItemAndServersMovieItem>>(json);
                if (animes == null)
                {
                    return new List<EpisodeItemAndServersMovieItem>();
                }
                else
                {
                    var orderedMovies = animes.OrderByDescending(a => a.MovieItem.DateAdded).ToList();
                    return orderedMovies;
                }
            }
            catch (FileNotFoundException)
            {
                return new List<EpisodeItemAndServersMovieItem>();
            }
        }

        public async Task DeletePlayedAsync(string KEY)
        {
            var recentPlayed = await GetRecentPlayedAsync();
            if (recentPlayed != null)
            {
                recentPlayed.RemoveAll(a => a.KEY == KEY);
                var json = JsonConvert.SerializeObject(recentPlayed);
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("recently_played_mvs.json", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, json);
            }
        }
    }
}
