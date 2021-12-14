using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class MovieResults
    {
        public List<Movie> Results { get; set; }

        public int TotalPages { get; set; }
    }

    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Overview { get; set; }

        public string PosterPath { get; set; }
        
        public string ReleaseDate { get; set; }
    }

    public class GenreList
    {
        public List<GenreDetails> Genres { get; set; }
    }

    public class GenreDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TotalPages
    {
        public int PageNumber { get; set; }
    }

    public class MovieToExclude
    {
        public int MovieId { get; set; }

        public int UserId { get; set; }

        public string Opinion { get; set; }

        public int RemovalTracker { get; set; }
    }
}
