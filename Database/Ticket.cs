using System;
using System.Collections.Generic;

namespace CinemaDB;

public partial class Ticket
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SessionId { get; set; }

    public int Seat { get; set; }

    public virtual Session Session { get; set; } = null!;

    public virtual ICollection<TakenSeat> TakenSeats { get; set; } = new List<TakenSeat>();

    public virtual User User { get; set; } = null!;
}
