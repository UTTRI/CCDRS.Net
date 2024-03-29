﻿/*
    Copyright 2023 University of Toronto
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

using CCDRSManager.Model;
using System.ComponentModel;
using System.IO;

namespace CCDRSManager;

/// <summary>
/// VehicleCountTypeModel class when the VehicleCountType model is changed.
/// </summary>
public class VehicleCountTypeModel : INotifyPropertyChanged
{
    private readonly VehicleCountType _vehicleCountType;

    /// <summary>
    /// Primary serial key of vehicle_count_type
    /// </summary>
    public int Id
    {
        get
        {
            return _vehicleCountType.Id;
        }
        set
        {
            _vehicleCountType.Id = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
        }
    }

    /// <summary>
    /// Description of the vehicle e.g. auto1.
    /// </summary>
    public string Description
    {
        get
        {
            return _vehicleCountType.Description;
        }
        set
        {
            _vehicleCountType.Description = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
        }
    }

    /// <summary>
    /// Number of occupants that can sit in the vehicle.
    /// </summary>
    public int Occupancy
    {
        get
        {
            return _vehicleCountType.Occupancy;
        }
        set
        {
            _vehicleCountType.Occupancy = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Occupancy)));
        }
    }

    /// <summary>
    /// Stores the type of vehicle. Used to determine drop down options
    /// </summary>
    public int CountType
    {
        get
        {
            return _vehicleCountType.CountType;
        }
        set
        {
            _vehicleCountType.CountType = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountType)));
        }
    }

    /// <summary>
    /// Foreign key to the vehicle table associated to the vehicle primary key attribute
    /// </summary>
    public int VehicleId
    {
        get
        {
            return _vehicleCountType.VehicleId;
        }
        set
        {
            _vehicleCountType.VehicleId = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VehicleId)));
        }
    }

    /// <summary>
    /// Name of Vehicle.
    /// </summary>
    public string VehicleName
    {
        get
        {
            return _vehicleCountType.Vehicle.Name;
        }
    }


    /// <summary>
    /// Initialize the class.
    /// </summary>
    /// <param name="vehicleCountType">Pass in a VehicleCountType object</param>
    public VehicleCountTypeModel(VehicleCountType vehicleCountType)
    {
        _vehicleCountType = vehicleCountType;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
