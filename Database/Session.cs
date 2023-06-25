using System;
using System.Collections.Generic;

namespace CinemaDB;

public partial class Session
{
    public int Id { get; set; }

    public DateTime DateTime { get; set; }

    public int MovieId { get; set; }

    public int HallId { get; set; }
    public int Price { get; set; }

    public virtual Hall Hall { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual Ticket? Ticket { get; set; }
}
