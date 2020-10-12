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
        /// <summary>
        /// For testing purpose
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SearchResult>> Get()
        {
            List<SearchResult> search_list = new List<SearchResult>();
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
                    int value = 0;
                    string urlstr = "";
                    if (t["collectionId"] != null)
                        value = (int)t["collectionId"];
                    if (t["collectionViewUrl"] != null)
                        urlstr = t["collectionViewUrl"].ToString();
                    if(!urlstr.Equals("") && value!=0)
                        search_list.Add(new SearchResult { CollectionId = value, CollectionViewUrl = urlstr });
                }
            }
            return search_list;
        }

        /// <summary>
        /// For book records
        /// the input is in string format like "fire","world"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("book/{id}")]
        public async Task<List<SearchResult>> GetBooks(String id)
        {
            List<SearchResult> search_list = new List<SearchResult>();
            if (id.Equals(""))
                return search_list;
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
                        search_list.Add(new SearchResult { TrackId = trackid, TrackViewURL = trackurl, TrackName = trackname, AverageUserRating=avgRating, ReleaseDate=releasedate, UserRatingCount=userratingcount, ArtistName=author, artistViewURL = authorurl, artworkUrl100 = artworkUrl, price =Price });
                }
            }
            return search_list;

        }

        /// <summary>
        /// For movie records
        /// the input is in string format like "fire", "world"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("movie/{id}")]
        public async Task<List<SearchResult>> GetMovies(String id)
        {
            List<SearchResult> search_list = new List<SearchResult>();
            if (id.Equals(""))
                return search_list;
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
                    string PreviewURL = "";
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
                    if (t["previewUrl"] != null)
                        PreviewURL = t["previewUrl"].ToString();
                    if (!trackurl.Equals("") && !contentadvisoryrating.Equals("") && !trackname.Equals("") && !primarygenrename.Equals("") && !releasedate.Equals("") && tracktimemillis != 0 && !author.Equals("") && !artworkUrl.Equals("") && Price!=0 && !PreviewURL.Equals(""))
                        search_list.Add(new SearchResult { TrackTimeMillis = tracktimemillis, TrackViewURL = trackurl, TrackName = trackname, ContentAdvisoryRating = contentadvisoryrating, ReleaseDate = releasedate, PrimaryGenreName = primarygenrename, ArtistName = author, artworkUrl100 = artworkUrl,trackPrice=Price, previewUrl = PreviewURL}) ;
                }
            }
            return search_list;

        }

        /// <summary>
        /// for musicVideo records
        /// the input is in string format like "fire","world"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("musicVideo/{id}")]
        public async Task<List<SearchResult>> GetMusicVideo(String id)
        {
            List<SearchResult> search_list = new List<SearchResult>();
            if (id.Equals(""))
                return search_list;
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
                        search_list.Add(new SearchResult { TrackViewURL = trackurl, TrackName = trackname, ReleaseDate = releasedate, ArtistName = author, artistViewURL = authorurl, PrimaryGenreName = primarygenrename, TrackTimeMillis = tracktimemillis, artworkUrl100 = artworkUrl, trackPrice = Price, previewUrl = PreviewURL }) ;
                }
            }
            return search_list;

        }

        /// <summary>
        /// For podcast records
        /// the input is in string format like "fire","world"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("podcast/{id}")]
        public async Task<List<SearchResult>> GetPodcast(String id)
        {
            List<SearchResult> search_list = new List<SearchResult>();
            if (id.Equals(""))
                return search_list;
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://itunes.apple.com/search?term=" + id + "&entity=podcast&limit=20");
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
                    string releasedate = "";
                    string author = "";
                    string contentadvisoryrating = "";
                    string artworkUrl = "";
                    if (t["artistName"] != null)
                        author = t["artistName"].ToString();
                    if (t["trackName"] != null)
                        trackname = t["trackName"].ToString();
                    if (t["artworkUrl100"] != null)
                        artworkUrl = t["artworkUrl100"].ToString();
                    if (t["trackViewUrl"] != null)
                        trackurl = t["trackViewUrl"].ToString();
                    if (t["contentAdvisoryRating"] != null)
                        contentadvisoryrating = t["contentAdvisoryRating"].ToString();
                    if (t["primaryGenreName"] != null)
                        primarygenrename = t["primaryGenreName"].ToString();
                    if (t["releaseDate"] != null)
                        releasedate = t["releaseDate"].ToString();
                    if (!trackurl.Equals("") && !contentadvisoryrating.Equals("") && !trackname.Equals("") && !primarygenrename.Equals("") && !releasedate.Equals("") && !author.Equals("") && !artworkUrl.Equals(""))
                        search_list.Add(new SearchResult { TrackViewURL = trackurl, TrackName = trackname, ContentAdvisoryRating = contentadvisoryrating, ReleaseDate = releasedate, PrimaryGenreName = primarygenrename, ArtistName = author, artworkUrl100 = artworkUrl });
                }
            }
            return search_list;

        }

        /// <summary>
        /// For audiobook records
        /// the input is in string format like "fire","world"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("audiobook/{id}")]
        public async Task<List<SearchResult>> GetAudioBook(String id)
        {
            List<SearchResult> search_list = new List<SearchResult>();
            if (id.Equals(""))
                return search_list;
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://itunes.apple.com/search?term=" + id + "&entity=audiobook&limit=20");
                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                    json = await content.ReadAsStringAsync();
                JObject json1 = JObject.Parse(json);
                foreach (var t in json1["results"])
                {
                    string primarygenrename = "";
                    string releasedate = "";
                    string author = "";
                    string artworkUrl = "";
                    string CollectionName = "";
                    string authorURL = "";
                    string collectionURL = "";
                    double collectionprice = 0;
                    string previewURL = "";
                    if (t["artistName"] != null)
                        author = t["artistName"].ToString();
                    if (t["collectionName"] != null)
                        CollectionName = t["collectionName"].ToString();
                    if (t["artistViewUrl"] != null)
                        authorURL = t["artistViewUrl"].ToString();
                    if (t["collectionViewUrl"] != null)
                        collectionURL = t["collectionViewUrl"].ToString();
                    if (t["artworkUrl100"] != null)
                        artworkUrl = t["artworkUrl100"].ToString();
                    if (t["collectionPrice"] != null)
                        collectionprice = (double)t["collectionPrice"];
                    if (t["previewUrl"] != null)
                        previewURL = t["previewUrl"].ToString();
                    if (t["primaryGenreName"] != null)
                        primarygenrename = t["primaryGenreName"].ToString();
                    if (t["releaseDate"] != null)
                        releasedate = t["releaseDate"].ToString();
                    if (!primarygenrename.Equals("") && !releasedate.Equals("") && !author.Equals("") && !artworkUrl.Equals("") && !CollectionName.Equals("") && !authorURL.Equals("") && !collectionURL.Equals("") && !previewURL.Equals("") && collectionprice!=0)
                        search_list.Add(new SearchResult { ReleaseDate = releasedate, PrimaryGenreName = primarygenrename, ArtistName = author, artworkUrl100 = artworkUrl, collectionName = CollectionName, artistViewURL = authorURL, CollectionViewUrl = collectionURL, collectionPrice = collectionprice, previewUrl = previewURL });
                }
            }
            return search_list;

        }

        /// <summary>
        /// For album records
        /// the input is in string format like "fire","world"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("album/{id}")]
        public async Task<List<SearchResult>> GetAlbum(String id)
        {
            List<SearchResult> search_list = new List<SearchResult>();
            if (id.Equals(""))
                return search_list;
            using (var client = new HttpClient())
            {
                var url = new Uri($"https://itunes.apple.com/search?term=" + id + "&entity=album&limit=20");
                var response = await client.GetAsync(url);

                string json;
                using (var content = response.Content)
                    json = await content.ReadAsStringAsync();
                JObject json1 = JObject.Parse(json);
                foreach (var t in json1["results"])
                {
                    string primarygenrename = "";
                    string releasedate = "";
                    string author = "";
                    string artworkUrl = "";
                    string CollectionName = "";
                    string authorURL = "";
                    string collectionURL = "";
                    double collectionprice = 0;
                    if (t["artistName"] != null)
                        author = t["artistName"].ToString();
                    if (t["collectionName"] != null)
                        CollectionName = t["collectionName"].ToString();
                    if (t["artistViewUrl"] != null)
                        authorURL = t["artistViewUrl"].ToString();
                    if (t["collectionViewUrl"] != null)
                        collectionURL = t["collectionViewUrl"].ToString();
                    if (t["artworkUrl100"] != null)
                        artworkUrl = t["artworkUrl100"].ToString();
                    if (t["collectionPrice"] != null)
                        collectionprice = (double)t["collectionPrice"];
                    if (t["primaryGenreName"] != null)
                        primarygenrename = t["primaryGenreName"].ToString();
                    if (t["releaseDate"] != null)
                        releasedate = t["releaseDate"].ToString();
                    if (!primarygenrename.Equals("") && !releasedate.Equals("") && !author.Equals("") && !artworkUrl.Equals("") && !CollectionName.Equals("") && !authorURL.Equals("") && !collectionURL.Equals("") && collectionprice != 0)
                        search_list.Add(new SearchResult { ReleaseDate = releasedate, PrimaryGenreName = primarygenrename, ArtistName = author, artworkUrl100 = artworkUrl, collectionName = CollectionName, artistViewURL = authorURL, CollectionViewUrl = collectionURL, collectionPrice = collectionprice });
                }
            }
            return search_list;

        }

    }
}
