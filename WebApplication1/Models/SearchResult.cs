﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    [DataContract]
    public class SearchResult
    {
        [DataMember(Name = "collectionId")]
        public int CollectionId { get; set; }
        //[DataMember(Nam? = "results")]
        //public int CollectionArtistId { get; set; }
        [DataMember(Name = "collectionViewUrl")]
        public string CollectionViewUrl { get; set; }

        [DataMember(Name = "trackId")]
        public int TrackId { get; set; }

        [DataMember(Name = "trackName")]
        public string TrackName { get; set; }

        [DataMember(Name = "trackViewUrl")]
        public string TrackViewURL { get; set; }

        [DataMember(Name = "averageUserRating")]
        public double AverageUserRating { get; set; }

        [DataMember(Name = "releaseDate")]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "userRatingCount")]
        public int UserRatingCount { get; set; }

        [DataMember(Name = "contentAdvisoryRating")]
        public string ContentAdvisoryRating { get; set; }

        [DataMember(Name = "trackTimeMillis")]
        public int TrackTimeMillis { get; set; }

        [DataMember(Name = "primaryGenreName")]

        public string PrimaryGenreName { get; set; }

        [DataMember(Name = "artistName")]
        public string ArtistName { get; set; }

        [DataMember(Name = "artistViewUrl")]
        public string artistViewURL { get; set; }

        [DataMember(Name = "artworkUrl100")]

        public string artworkUrl100 { get; set; }

        [DataMember(Name = "price")]
        public double price { get; set; }

        [DataMember(Name = "trackPrice")]
        public double trackPrice { get; set; }
    }
}
