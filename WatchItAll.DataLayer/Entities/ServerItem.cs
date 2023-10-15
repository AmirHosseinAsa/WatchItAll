namespace WatchItAll.DataLayer.Entities
{
    public class ServerItem
    {
        public string ServerTitle { get; set; }
        public string ServerURL { get; set; }

        public ServerItem(string serverTitle, string serverUrl)
        {
            this.ServerTitle = serverTitle;
            this.ServerURL = serverUrl;
        }
    }
}
