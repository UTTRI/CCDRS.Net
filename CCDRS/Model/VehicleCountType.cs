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

public partial class VehicleCountType
{
    /// <summary>
    /// Primary serial key of type int that is auto generated
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Stores the number of passengers that can be occupied by a vehicle type
    /// </summary>
    public int Occupancy { get; set; }

    /// <summary>
    /// Foreign key to the vehicle table connected to the vehicle primary key 
    /// </summary>
    public int VehicleId { get; set; }

    /// <summary>
    /// Human readable description of the vehicle.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Stores the type of vehicle. Used to determine drop down options
    /// </summary>
    public int CountType { get; set; }
}
