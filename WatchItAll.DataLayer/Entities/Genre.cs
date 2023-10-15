namespace WatchItAll.DataLayer.Entities
{
    public class Genre
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public Genre(string Name, string Link)
        {
            this.Name = Name;
            this.Link = Link;
        }
    }
}
