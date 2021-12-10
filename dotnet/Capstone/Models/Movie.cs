﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class MovieResults
    {
        public List<Movie> Results { get; set; }
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
}
