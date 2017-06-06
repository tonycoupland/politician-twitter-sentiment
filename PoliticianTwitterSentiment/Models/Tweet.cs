using System;
namespace PoliticianTwitterSentiment.Models
{
    public class Tweet
    {
        public Tweet()
        {
        }

        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }

        public string TwitterHandle { get; set; }

        public string Party { get; set; }

        public decimal Sentiment { get; set; }
    }
}


