using System;
using System.Collections.Generic;

namespace CinemaDB;

public partial class Genre
{
    public int Id { get; set; }

    public string GenreName { get; set; } = null!;

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
