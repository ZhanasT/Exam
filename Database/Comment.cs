using System;
using System.Collections.Generic;

namespace CinemaDB;

public partial class Comment
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? CommentText { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
