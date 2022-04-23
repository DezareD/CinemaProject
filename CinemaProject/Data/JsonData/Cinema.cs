namespace CinemaProject.Data.JsonData
{
    public class CinamePackage 
    {
        public int total { get; set; }
        public int totalPages { get; set; }

        public List<Cinema> items { get; set; }
        public List<Cinema> lastCinemas { get; set; }
    }

    public class CinemaTops
    {
        public int pagesCount { get; set; }
        public List<Cinema> films { get; set; }
    }

    public class Cinema
    {
        public long? kinopoiskId { get; set; }
        public string imdbId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }

        public string nameOriginal { get; set; }

        public List<Country>? countries { get; set; }

        public List<Genre>? genres { get; set; }

        public double? ratingKinopoisk { get; set; }
        public double? ratingImdb { get; set; }

        public int? year { get; set; }

        public string type { get; set; }

        public string posterUrl { get; set; }

        public string posterUrlPreview { get; set; }
    }

    public class Country
    {
        public string country { get; set; }
    }

    public class Genre
    {
        public string genre { get; set; }
    }
}
