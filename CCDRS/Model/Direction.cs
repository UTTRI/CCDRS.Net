using System;
using System.Collections.Generic;

namespace CCDRS.Model;

public partial class Direction
{
    public int Id { get; set; }

    public string Compass { get; set; } = null!;

    public char Abbr { get; set; }
}
