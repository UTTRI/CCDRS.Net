using System;
using System.Collections.Generic;

namespace CCDRS.Model;

/// <summary>
/// Class that maps to the vehicle table
/// </summary>
public partial class Vehicle
{
    /// <summary>
    /// Id is primary serial key of type int that is auto generated
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Stores the name of the vehicle, e.g. auto
    /// </summary>
    public string Name { get; set; } = string.Empty!;
}
