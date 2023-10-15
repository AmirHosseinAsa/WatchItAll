using System;

namespace WatchItAll.DataLayer.Entities
{
    public class Search
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Search(string Text, DateTime Date)
        {
            this.Text = Text;
            this.Date = Date;
        }
    }
}
