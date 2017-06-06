using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ArrayOfBytes.OAuth.Client;
using ArrayOfBytes.TeensyTwitter;

namespace PoliticianTwitterSentiment.Controllers
{
    [Route("api/[controller]")]
    public class PoliticiansTweetsController : Controller
	{
        public IConfiguration Configuration { get; set; }
        public PoliticiansTweetsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


		// GET api/politicianstweets
		[HttpGet]
        public async Task<IActionResult> Get()
        {
            Console.WriteLine("Requesting tweets");

            var configSection = Configuration.GetSection("TwitterApiAuth");
            if ( configSection == null){
                return StatusCode(500, "Twitter API Authentication details not found");
            }

            Console.WriteLine("ConsumerKey = " + configSection.GetValue<string>("ConsumerKey"));

            // Connect to twitter API
            OAuthConfig config = new OAuthConfig(configSection.GetValue<string>("ConsumerKey"), // Key
                                                 configSection.GetValue<string>("ConsumerSecret"), // Secret
                                                 configSection.GetValue<string>("AccessToken"), // Access token
                                                 configSection.GetValue<string>("AccessTokenSecret") // Access secret
                                                );

			TwitterClient.TwitterClient client = new TwitterClient.TwitterClient(config);

			// Get latest tweets
			var tweets = await client.HomeTimelineStatuses(50);

            // Return
            return Ok(tweets.Select(t => new Models.Tweet()
            {
                Id = t.Id,
                CreatedAt = t.CreatedAt,
                Text = t.Text,
                TwitterHandle = t.User.Name,
                Sentiment = Sentiment.Sentiment.Instance.GetScore(t.Text).SentimentNormalised,
                Party = "??"
            }));
        }
    }
}
