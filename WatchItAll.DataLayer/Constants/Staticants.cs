using System.Collections.Generic;
using WatchItAll.DataLayer.Entities;

namespace WatchItAll.DataLayer.Constants
{
    public class Staticants
    {
        public static string baseUrl = "https://moviestowatch.tv/";
        public static string search = "search/";

        static string ACTION =  "genre/action";
        static string ACTION_ADVENTURE =  "genre/action-adventure";
        static string ADVENTURE =  "genre/adventure";
        static string ANIMATION =  "genre/animation";
        static string BIOGRAPHY =  "genre/biography";
        static string COMEDY =  "genre/comedy";
        static string CRIME =  "genre/crime";
        static string DOCUMENTARY =  "genre/documentary";
        static string DRAMA =  "genre/drama";
        static string FAMILY =  "genre/family";
        static string FANTASY =  "genre/fantasy";
        static string HISTORY =  "genre/history";
        static string HORROR =  "genre/horror";
        static string KIDS =  "genre/kids";
        static string MUSIC =  "genre/music";
        static string MYSTERY =  "genre/mystery";
        static string NEWS =  "genre/news";
        static string REALITY=  "genre/reality";
        static string ROMANCE =  "genre/romance";
        static string SCIFI_FANTASY =  "genre/sci-fi-fantasy";
        static string SCIENCEFICTION=  "genre/science-fiction";
        static string SOAP =  "genre/soap";
        static string TALK =  "genre/talk";
        static string THRILLER =  "genre/thriller";
        static string TV_MOVIE =  "genre/tv-movie";
        static string WAR =  "genre/war";
        static string WAR_POLITICS =  "genre/war-politics";
        static string WESTERN =  "genre/western";

        public static List<Genre> GenreList = new List<Genre>() {
    new Genre(
        Name: "Action",
        Link: ACTION
    ),
    new Genre(
        Name: "Action & Adventure",
        Link: ACTION_ADVENTURE
    ),
    new Genre(
        Name: "Adventure",
        Link: ADVENTURE
    ),
    new Genre(
        Name: "Animation",
        Link: ANIMATION
    ),
    new Genre(
        Name: "Biography",
        Link: BIOGRAPHY
    ),
    new Genre(
        Name: "Comedy",
        Link: COMEDY
    ),
    new Genre(
        Name: "Crime",
        Link: CRIME
    ),
    new Genre(
        Name: "Documentary",
        Link: DOCUMENTARY
    ),
    new Genre(
        Name: "Drama",
        Link: DRAMA
    ),
    new Genre(
        Name: "Family",
        Link: FAMILY
    ),
    new Genre(
        Name: "Fantasy",
        Link: FANTASY
    ),
    new Genre(
        Name: "History",
        Link: HISTORY
    ),
    new Genre(
        Name: "Horror",
        Link: HORROR
    ),
    new Genre(
        Name: "Kids",
        Link: KIDS
    ),
    new Genre(
        Name: "Music",
        Link: MUSIC
    ),
    new Genre(
        Name: "Mystery",
        Link: MYSTERY
    ),
    new Genre(
        Name: "News",
        Link: NEWS
    ),
    new Genre(
        Name: "Reality",
        Link: REALITY
    ),
    new Genre(
        Name: "Romance",
        Link: ROMANCE
    ),
    new Genre(
        Name: "Sci-Fi & Fantasy",
        Link: SCIFI_FANTASY
    ),
    new Genre(
        Name: "Science Fiction",
        Link: SCIENCEFICTION
    ),
    new Genre(
        Name: "Soap",
        Link: SOAP
    ),
    new Genre(
        Name: "Talk",
        Link: TALK
    ),
    new Genre(
        Name: "Thriller",
        Link: THRILLER
    ),
    new Genre(
        Name: "TV Movie",
        Link: TV_MOVIE
    ),
    new Genre(
        Name: "War",
        Link: WAR
    ),
    new Genre(
        Name: "War Politics",
        Link: WAR_POLITICS
    ),
    new Genre(
        Name: "Western",
        Link: WESTERN
    ),
};
    }
}
