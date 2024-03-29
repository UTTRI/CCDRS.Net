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

namespace CCDRSManager.Model;

/// <summary>
/// Class that maps to the survey table
/// </summary>
public partial class Survey
{
    /// <summary>
    /// Primary serial key of type int that is auto generated
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The year which the survey was conducted in e.g. 2016. 
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Foreign key to the region table associated to the region primary key attribute
    /// </summary>
    public int RegionId { get; set; }

    /// <summary>
    /// Survey notes and calculations for total vehicle and total pedestrian. 
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Modify Region Method since survey has a foreign key to region table. 
    /// </summary>
    public virtual Region Region { get; set; } = null!;

    /// <summary>
    /// Collection of survey_stations associated with survey
    /// </summary>
    public virtual ICollection<SurveyStation> SurveyStations { get; } = new List<SurveyStation>();
}
