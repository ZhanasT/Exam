namespace CinemaDB.DataModels;

public class SessionForTimeTable
{
    public string MovieTitle { get; set; }
    public TimeOnly StartTime { get; set; }
    public int HallNumber { get; set; }
    public int Price { get; set; }
    public int MovieId { get; set; }
}
