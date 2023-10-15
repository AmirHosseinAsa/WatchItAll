using System;

namespace WatchItAll.DataLayer.Entities
{
    public class MovieItem
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string IMDBRate { get; set; }
        public string Year { get; set; }
        public MovieDetail Detail { get; set; }
        public DateTime DateAdded { get; set; }
        public string Columns { get; set; } = "4";
        public MovieItem(string name, string imageUrl, string summary, string url,string imdbRate, string year)
        {
            this.Name = name ?? null;
            this.ImageUrl = imageUrl ?? null;
            this.Summary = summary ?? null;
            this.Url = url ?? null;
            this.IMDBRate = imdbRate ?? null;
            this.Year = year ?? null;
        }
    }
}
