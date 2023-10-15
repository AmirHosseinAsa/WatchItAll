using System.Collections.Generic;

namespace WatchItAll.DataLayer.Entities
{
    public class SeasonItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<EpisodeItem> Episodes { get; set; }

        public SeasonItem(string id, string title, List<EpisodeItem> episodes)
        {
            this.Id = id ?? "";
            this.Title = title ?? "";
            this.Episodes = episodes ?? null;
        }
    }
}
