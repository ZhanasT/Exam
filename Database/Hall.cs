using System;
using System.Collections.Generic;

namespace CinemaDB;

public partial class Hall
{
    public int Id { get; set; }

    public int AmountOfSeats { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
