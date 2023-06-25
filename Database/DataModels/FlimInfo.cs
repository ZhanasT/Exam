namespace CinemaDB.DataModels;

public class MovieInfo
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string PosterSrc { get; set; }
    public List<string> Genres { get; set; }
    public int AgeLimit { get; set; }
    public int Duration { get; set; }
    public string Director { get; set; }
    public List<string> Actors { get; set; }
    public double Rating { get; set; }
}
