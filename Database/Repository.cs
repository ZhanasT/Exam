using Microsoft.EntityFrameworkCore;

namespace CinemaDB;

public class Repository
{
    private MoviesDbContext db = new MoviesDbContext();

    public async Task<List<Session>> GetMovies()
    {
        return await (from session in db.Sessions
                     where session.MovieId == 1
                     select session).ToListAsync();
    }
}
