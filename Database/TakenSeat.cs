using System;
using System.Collections.Generic;

namespace CinemaDB;

public partial class TakenSeat
{
    public int Id { get; set; }

    public int SessionId { get; set; }

    public int Seat { get; set; }

    public virtual Ticket SeatNavigation { get; set; } = null!;
}
