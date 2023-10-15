namespace WatchItAll.DataLayer.Entities
{
    public class StreamURlForMovieItem
    {
        public string ServerNumber { get; set; }
        public string ServerId { get; set; }
        public StreamURlForMovieItem(string serverNumber, string serverId)
        {
            this.ServerNumber = serverNumber;
            this.ServerId = serverId;
        }
    }
}
