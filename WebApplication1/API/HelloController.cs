using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        // For testing purposes
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
        //For ebook records
        [HttpGet("book/{id}")]
        public async Task<List<SearchResult>> GetBooks(String id)
        {
            List<SearchResult> temp = new List<SearchResult>();
            if (id.Equals(""))
                return temp;
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://itunes.apple.com/search?term=" + id +"&entity=ebook&limit=20");
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
                    string author = "";
                    string authorurl = "";
                    double Price = 0;
                    string artworkUrl = "";
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
                    if (t["artistName"] != null)
                        author = t["artistName"].ToString();
                    if (t["artistViewUrl"] != null)
                        authorurl = t["artistViewUrl"].ToString();
                    if (t["artworkUrl100"] != null)
                        artworkUrl = t["artworkUrl100"].ToString();
                    if (t["price"] != null)
                    {
                        Price = (double)t["price"];
                    }
                    if (!trackurl.Equals("") && trackid != 0 && !trackname.Equals("") && avgRating!=0 && !releasedate.Equals("") && userratingcount!=0 && !author.Equals("") && !authorurl.Equals("") && !artworkUrl.Equals("") && Price!=0)
                        temp.Add(new SearchResult { TrackId = trackid, TrackViewURL = trackurl, TrackName = trackname, AverageUserRating=avgRating, ReleaseDate=releasedate, UserRatingCount=userratingcount, ArtistName=author, artistViewURL = authorurl, artworkUrl100 = artworkUrl, price =Price });
                }
            }
            return temp;

        }

        // GET api/search/movie/5
        //For movie records
        [HttpGet("movie/{id}")]
        public async Task<List<SearchResult>> GetMovies(String id)
        {
            List<SearchResult> temp = new List<SearchResult>();
            if (id.Equals(""))
                return temp;
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://itunes.apple.com/search?term=" + id + "&entity=movie&limit=20");
                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                    json = await content.ReadAsStringAsync();
                JObject json1 = JObject.Parse(json);
                foreach (var t in json1["results"])
                {
                    string trackurl = "";
                    string trackname = "";
                    string primarygenrename = "";
                    int tracktimemillis = 0;
                    string releasedate = "";
                    string author = "";
                    string contentadvisoryrating = "";
                    string artworkUrl = "";
                    double Price = 0;
                    if (t["contentAdvisoryRating"] != null)
                        contentadvisoryrating = t["contentAdvisoryRating"].ToString();
                    if (t["trackName"] != null)
                        trackname = t["trackName"].ToString();
                    if (t["trackViewUrl"] != null)
                        trackurl = t["trackViewUrl"].ToString();
                    if (t["trackTimeMillis"] != null)
                        tracktimemillis = (int)t["trackTimeMillis"];
                    if (t["releaseDate"] != null)
                        releasedate = t["releaseDate"].ToString();
                    if (t["artistName"] != null)
                        author = t["artistName"].ToString();
                    if (t["primaryGenreName"] != null)
                        primarygenrename = t["primaryGenreName"].ToString();
                    if (t["artworkUrl100"] != null)
                        artworkUrl = t["artworkUrl100"].ToString();
                    if (t["trackPrice"] != null)
                        Price = (double)t["trackPrice"];
                    if (!trackurl.Equals("") && !contentadvisoryrating.Equals("") && !trackname.Equals("") && !primarygenrename.Equals("") && !releasedate.Equals("") && tracktimemillis != 0 && !author.Equals("") && !artworkUrl.Equals("") && Price!=0)
                        temp.Add(new SearchResult { TrackTimeMillis = tracktimemillis, TrackViewURL = trackurl, TrackName = trackname, ContentAdvisoryRating = contentadvisoryrating, ReleaseDate = releasedate, PrimaryGenreName = primarygenrename, ArtistName = author, artworkUrl100 = artworkUrl,trackPrice=Price }) ;
                }
            }
            return temp;

        }

        // GET api/search/musicVideo/5
        //For musicVideo records
        [HttpGet("musicVideo/{id}")]
        public async Task<List<SearchResult>> GetMusicVideo(String id)
        {
            List<SearchResult> temp = new List<SearchResult>();
            if (id.Equals(""))
                return temp;
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://itunes.apple.com/search?term=" + id + "&entity=musicVideo&limit=20");
                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                    json = await content.ReadAsStringAsync();
                JObject json1 = JObject.Parse(json);
                foreach (var t in json1["results"])
                {
                    string trackurl = "";
                    string trackname = "";
                    string releasedate = "";
                    string author = "";
                    string authorurl = "";
                    string primarygenrename = "";
                    int tracktimemillis = 0;
                    string artworkUrl = "";
                    double Price = 0;
                    string PreviewURL = "";
                    if (t["trackName"] != null)
                        trackname = t["trackName"].ToString();
                    if (t["trackViewUrl"] != null)
                        trackurl = t["trackViewUrl"].ToString();
                    if (t["releaseDate"] != null)
                        releasedate = t["releaseDate"].ToString();
                    if (t["artistName"] != null)
                        author = t["artistName"].ToString();
                    if (t["artistViewUrl"] != null)
                        authorurl = t["artistViewUrl"].ToString();
                    if (t["primaryGenreName"] != null)
                        primarygenrename = t["primaryGenreName"].ToString();
                    if (t["trackTimeMillis"] != null)
                        tracktimemillis = (int)t["trackTimeMillis"];
                    if (t["artworkUrl100"] != null)
                        artworkUrl = t["artworkUrl100"].ToString();
                    if (t["trackPrice"] != null)
                        Price = (double)t["trackPrice"];
                    if (t["previewUrl"] != null)
                        PreviewURL = t["previewUrl"].ToString();
                    if (!trackurl.Equals("") && !trackname.Equals("") && !releasedate.Equals("") && !author.Equals("") && !authorurl.Equals("") && !primarygenrename.Equals("") && tracktimemillis != 0 && !artworkUrl.Equals("") && Price != 0 && !PreviewURL.Equals(null))
                        temp.Add(new SearchResult { TrackViewURL = trackurl, TrackName = trackname, ReleaseDate = releasedate, ArtistName = author, artistViewURL = authorurl, PrimaryGenreName = primarygenrename, TrackTimeMillis = tracktimemillis, artworkUrl100 = artworkUrl, trackPrice = Price, previewUrl = PreviewURL }) ;
                }
            }
            return temp;

        }

    }
}
