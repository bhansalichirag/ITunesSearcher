using System;
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
        public int? CollectionId { get; set; }
        //[DataMember(Nam? = "results")]
        //public int CollectionArtistId { get; set; }
        [DataMember(Name = "collectionViewUrl")]
        public string? CollectionViewUrl { get; set; }

        [DataMember(Name = "trackId")]
        public int? TrackId { get; set; }

        [DataMember(Name = "trackName")]
        public string? TrackName { get; set; }

        [DataMember(Name = "trackViewUrl")]
        public string? TrackViewURL { get; set; }

        [DataMember(Name = "averageUserRating")]
        public double? AverageUserRating { get; set; }

        [DataMember(Name = "releaseDate")]
        public string? ReleaseDate { get; set; }

        [DataMember(Name = "userRatingCount")]
        public int? UserRatingCount { get; set; }

        [DataMember(Name = "contentAdvisoryRating")]
        public string? ContentAdvisoryRating { get; set; }

        [DataMember(Name = "trackTimeMillis")]
        public int? TrackTimeMillis { get; set; }

        [DataMember(Name = "primaryGenreName")]

        public string PrimaryGenreName { get; set; }

    }
}
