using System;
using System.Collections.Generic;

namespace asp.netwebApiTask_8_24_2024_.Models;

public partial class UserRole
{
    public int UserId { get; set; }

    public string Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
