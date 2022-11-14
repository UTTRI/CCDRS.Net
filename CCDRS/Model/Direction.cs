﻿/*
    Copyright 2022 University of Toronto
    This file is part of CCDRS.
    CCDRS is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    CCDRS is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with CCDRS.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCDRS.Model;
/// <summary>
/// Class that maps to the direction table
/// </summary>
public partial class Direction
{
    /// <summary>
    /// Id is a Primary serial key of type int
    /// It is auto generated. 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Compass string generated of the name of directions eg North, South
    /// </summary>
    public string Compass { get; set; } = String.Empty;

    /// <summary>
    /// Abbreviation string generated of the abbreviated form of each direction
    /// Changed the attribute name from Abbr to Abbreviation
    /// </summary>
    [Column("Abbr")]
    public char Abbreviation { get; set; }
}
