
using ImdbWebApi.Models.DbModels;
using System;
using System.Collections.Generic;

namespace ImdbWebApi.Test.MockResources
{
    public class MockList
    {
        public static List<ActorDb> GetActorsList()
        {
            return new List<ActorDb>()
            {
                new ActorDb()
                {
                    Id = 1,
                    Name = "Mock Actor 1",
                    DOB = new DateTime(2002, 4, 4),
                    Bio = "Sample bio for actor",
                    GenderId = 1
                },
                new ActorDb()
                {
                    Id = 2,
                    Name = "Mock Actor 2",
                    DOB = new DateTime(1992, 2, 4),
                    Bio = "Sample bio for actor",
                    GenderId = 1
                },
                new ActorDb()
                {
                    Id = 3,
                    Name = "Mock Actor 3",
                    DOB = new DateTime(2000, 4, 4),
                    Bio = "Sample bio for actor",
                    GenderId = 1
                }
            };
        }

        public static List<GenderDb> GetGendersList()
        {
            return new List<GenderDb>()
            {
                new GenderDb()
                {
                    Id = 1,
                    Name = "Mock Gender 1"
                },
                new GenderDb()
                {
                    Id = 2,
                    Name = "Mock Gender 2"
                }
            };
        }

        public static List<GenreDb> GetGenresList()
        {
            return new List<GenreDb>()
            {
                new GenreDb()
                {
                    Id = 1,
                    Name = "Mock Genre 1"
                },
                new GenreDb()
                {
                    Id = 2,
                    Name = "Mock Genre 2"
                },
                new GenreDb()
                {
                    Id = 3,
                    Name = "Mock Genre 3"
                }
            };
        }

        public static List<MovieDb> GetMoviesList()
        {
            return new List<MovieDb>()
            {
                new MovieDb()
                {
                    Id = 1,
                    Name = "Mock Movie 1",
                    YearOfRelease = 2002,
                    Plot = "Mock plot for the movie",
                    ProducerId = 1,
                    ActorIds = "1,2",
                    GenreIds = "1,2",
                    CoverImage = "Path for cover image"
                },
                new MovieDb()
                {
                    Id = 2,
                    Name = "Mock Movie 2",
                    YearOfRelease = 2020,
                    Plot = "Mock plot for the movie",
                    ProducerId = 2,
                    ActorIds = "1,2",
                    GenreIds = "1,2",
                    CoverImage = "Path for cover image"
                }
            };
        }

        public static List<ProducerDb> GetProducersList()
        {
            return new List<ProducerDb>()
            {
                new ProducerDb()
                {
                    Id = 1,
                    Name = "Mock Producer 1",
                    DOB = new DateTime(2002, 4, 4),
                    Bio = "Sample bio for producer",
                    GenderId = 1
                },
                new ProducerDb()
                {
                    Id = 2,
                    Name = "Mock Producer 2",
                    DOB = new DateTime(1989, 7, 3),
                    Bio = "Sample bio for producer",
                    GenderId = 1
                },
                new ProducerDb()
                {
                    Id = 3,
                    Name = "Mock Producer 3",
                    DOB = new DateTime(2000, 4, 4),
                    Bio = "Sample bio for producer",
                    GenderId = 1
                }
            };
        }

        public static List<ReviewDb> GetReviewsList()
        {
            return new List<ReviewDb>()
            {
                new ReviewDb()
                {
                    Id = 1,
                    MovieId = 1,
                    Message = "Review for movie id 1"
                }
            };
        }
    }
}
