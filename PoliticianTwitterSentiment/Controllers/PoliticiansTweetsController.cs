using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PoliticianTwitterSentiment.Controllers
{
    [Route("api/[controller]")]
    public class PoliticiansTweetsController : Controller
    {
		// GET api/politicianstweets
		[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
