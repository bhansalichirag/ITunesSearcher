using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.API
{
    [Route("api/search")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        // GET: api/search
        [HttpGet]
        public async Task<List<SearchResult>> Get()
        {
            List<SearchResult> temp = new List<SearchResult>();
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://itunes.apple.com/search?term=Big&entity=ebook&limit=20");
                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                    json = await content.ReadAsStringAsync();
                JObject json1 = JObject.Parse(json);
                foreach (var t in json1["results"])
                {
                    int tempint = 0;
                    string urlstr = "";
                    if (t["collectionId"] != null)
                        tempint = (int)t["collectionId"];
                    if (t["collectionViewUrl"] != null)
                        urlstr = t["collectionViewUrl"].ToString();
                    if(!urlstr.Equals("") && tempint!=0)
                        temp.Add(new SearchResult { CollectionId = tempint, CollectionViewUrl = urlstr });
                }
            }
            return temp;
        }

        // GET api/search/book/5
        [HttpGet("book/{id}")]
        public async Task<List<SearchResult>> GetBooks(String id)
        {
            List<SearchResult> temp = new List<SearchResult>();
            if (id.Equals(""))
                return temp;
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://itunes.apple.com/search?term=" + id +"&entity=ebook");
                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                    json = await content.ReadAsStringAsync();
                JObject json1 = JObject.Parse(json);
                foreach (var t in json1["results"])
                {
                    int trackid = 0;
                    string trackurl = "";
                    string trackname = "";
                    double avgRating = 0;
                    string releasedate = "";
                    int userratingcount = 0;
                    if (t["trackId"] != null)
                        trackid = (int)t["trackId"];
                    if (t["trackName"] != null)
                        trackname = t["trackName"].ToString();
                    if (t["trackViewUrl"] != null)
                        trackurl = t["trackViewUrl"].ToString();
                    if (t["averageUserRating"] != null)
                        avgRating = (double)t["averageUserRating"];
                    if (t["releaseDate"] != null)
                        releasedate = t["releaseDate"].ToString();
                    if (t["userRatingCount"] != null)
                        userratingcount = (int)t["userRatingCount"];
                    if (!trackurl.Equals("") && trackid != 0 && !trackname.Equals("") && avgRating!=0 && !releasedate.Equals("") && userratingcount!=0)
                        temp.Add(new SearchResult { TrackId = trackid, TrackViewURL = trackurl, TrackName = trackname, AverageUserRating=avgRating, ReleaseDate=releasedate, UserRatingCount=userratingcount });
                }
            }
            return temp;

        }
    }
}
