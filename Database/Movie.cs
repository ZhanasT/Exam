using System;
using System.Collections.Generic;

namespace CinemaDB;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int AgeLimit { get; set; }

    public int Duration { get; set; }

    public double Rating { get; set; }

    public string PosterSrc { get; set; } = null!;

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Director> Directors { get; set; } = new List<Director>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
