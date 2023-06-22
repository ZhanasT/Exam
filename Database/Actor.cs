using System;
using System.Collections.Generic;

namespace CinemaDB;

public partial class Actor
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
