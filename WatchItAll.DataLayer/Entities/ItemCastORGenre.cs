namespace WatchItAll.DataLayer.Entities
{
    public class ItemCastORGenre
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public ItemCastORGenre(string title, string url)
        {
            this.Title = title ?? null;
            this.Url = url ?? null;
        }
    }
}
