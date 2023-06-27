using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CinemaDB.DataModels;

namespace CinemaDB;

public class Repository
{
    private MoviesDbContext db = new MoviesDbContext();

    public List<SessionForTimeTable> GetTimeTable(int day)
    {
        var sessions = new List<SessionForTimeTable>();

        var sessionsWithMovies = db.Sessions.Include(s => s.Movie).Where(s => s.DateTime.Day == day);
        
        foreach (var session in sessionsWithMovies)
        {
            var sessionToAdd = new SessionForTimeTable();
            sessionToAdd.MovieTitle = session.Movie.Title;
            sessionToAdd.HallNumber = session.HallId;
            sessionToAdd.Price = session.Price;
            sessionToAdd.MovieId = session.MovieId;
            var startTime = new TimeOnly(session.DateTime.Hour, session.DateTime.Minute, session.DateTime.Second);
            sessionToAdd.StartTime = startTime;
            sessions.Add(sessionToAdd);
        }
        return sessions;
    }
    public List<SessionForTimeTable> GetTimeTable(int day, int movieId)
    {
        var sessions = new List<SessionForTimeTable>();

        var sessionsWithMovies = db.Sessions.Include(s => s.Movie).Where(s => s.DateTime.Day == day && s.MovieId == movieId);
        
        foreach (var session in sessionsWithMovies)
        {
            var sessionToAdd = new SessionForTimeTable();
            sessionToAdd.MovieTitle = session.Movie.Title;
            sessionToAdd.HallNumber = session.HallId;
            sessionToAdd.Price = session.Price;
            sessionToAdd.MovieId = session.MovieId;
            var startTime = new TimeOnly(session.DateTime.Hour, session.DateTime.Minute, session.DateTime.Second);
            sessionToAdd.StartTime = startTime;
            Console.WriteLine(session.DateTime.Day);
            sessions.Add(sessionToAdd);
        }
        return sessions;
    }
    
    public MovieInfo GetMovieInfo(int movieId) 
    {
        var movieInfo = new MovieInfo();

        var movieFromDB = db.Movies.Find(movieId);
        movieInfo.Title = movieFromDB.Title;
        movieInfo.AgeLimit = movieFromDB.AgeLimit;
        movieInfo.Duration = movieFromDB.Duration;
        movieInfo.Description = movieFromDB.Description;
        movieInfo.Rating = movieFromDB.Rating;
        movieInfo.PosterSrc = movieFromDB.PosterSrc;

        var genres = db.Genres.Where(g => g.MovieId == movieId).Select(g => g.GenreName).ToList();
        var actors = db.Actors.Where(a => a.MovieId == movieId).Select(a => a.FullName).ToList();
        var director = db.Directors.Where(d => d.MovieId == movieId).Select(d => d.FullName).First();
        
        movieInfo.Genres = genres;
        movieInfo.Actors = actors;
        movieInfo.Director = director;

        return movieInfo;
    }
    public List<DataModels.Comment> GetCommentsByMovie(int movieId)
    {
        return db.Comments.Where(c => c.MovieId == movieId).Select(c => 
            new DataModels.Comment {
                UserName = c.Username,
                CommentText = c.CommentText
            }).ToList();

    }
    public async void AddUser(string login, string password)
    {
        int lastId = 0;
        if (db.Users.Any())
        {
            lastId = (from user in db.Users
                      orderby user.Id
                      select user.Id).Last();
            var userToAdd = new User();
            userToAdd.Login = login;
            userToAdd.Password = password;
            userToAdd.Id = lastId + 1;
            db.Users.Add(userToAdd);
            await db.SaveChangesAsync();
        }
        else 
        {
            var userToAdd = new User();
            userToAdd.Login = login;
            userToAdd.Password = password;
            userToAdd.Id = lastId + 1;
            db.Users.Add(userToAdd);
            await db.SaveChangesAsync();
        }

        
    }
    public User? SearchUser(string login, string password)
    {
        if (db.Users.Any(u => u.Login == login && u.Password == password))
        {
            return db.Users.Where(u => u.Login == login).ToList().First();
        }
        else
        {
            return null;
        }
        
    } 
}
