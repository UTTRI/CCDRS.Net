using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCDRS.Model;
/// <summary>
/// Class that maps to the postgresql direction table
/// It has three fields 
/// Id: Primary serial key
/// Compass: Key that holds the name of direction eg North, South
/// Abbr: An Abbreviation of the direction eg N, E, S, W
/// </summary>
public partial class Direction
{
    public int Id { get; set; }

    public string Compass { get; set; } = null!;

    [Column("Abbrevation")]
    public char Abbr { get; set; }
}
