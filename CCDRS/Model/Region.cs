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

namespace CCDRS.Model;

/// <summary>
/// Class that maps to the region table
/// </summary>
public partial class Region
{
    /// <summary>
    /// Primary serial key of type int that is auto generated
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Stores the name of the region, e.g. Toronto
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Collection of surveys associated with the region.
    /// </summary>
    public virtual ICollection<Survey> Surveys { get; } = new List<Survey>();
}
