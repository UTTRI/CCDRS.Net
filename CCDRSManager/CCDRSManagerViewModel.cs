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

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CCDRSManager;

/// <summary>
/// Manage and provides executes the various use cases. 
/// </summary>
public class CCDRSManagerViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// ccdrsRepository variable used and accessed by all methods.
    /// </summary>
    private readonly CCDRSManagerModelRepository _ccdrsRepository = Configuration.CCDRSManagerModelRepository;

    /// <summary>
    /// Observable List of all regions that exist in the database.
    /// </summary>
    public ReadOnlyObservableCollection<RegionModel> Regions { get; }

    private int _regionId;

    private int _surveyYear;

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Set the value of the user selected RegionId
    /// </summary>
    public int RegionId
    {
        get
        {
            return _regionId;
        }
        set
        {
            _regionId = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegionId)));
        }
    }

    /// <summary>
    /// The survey year the user inputs.
    /// </summary>
    public int SurveyYear
    {
        get
        {
            return _surveyYear;
        }
        set
        {
            _surveyYear = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SurveyYear)));
        }
    }

    private string _stationFileName = string.Empty;

    /// <summary>
    /// Name of stationfile csv file.
    /// </summary>
    public string StationFileName
    {
        get
        {
            return _stationFileName;
        }
        set
        {
            _stationFileName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StationFileName)));
        }

    }

    private string _stationCountObservationFile = string.Empty;

    /// <summary>
    /// Path to StationCountObservation CCDRS csv file.
    /// </summary>
    public string StationCountObservationFile
    {
        get
        {
            return _stationCountObservationFile;
        }
        set
        {
            _stationCountObservationFile = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StationCountObservationFile)));
        }
    }

    /// <summary>
    /// Property of the progress bar to check if it is running or not.
    /// </summary>
    private bool _isRunning;
    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
        }
    }

    private string _screenlineFileName = string.Empty;

    /// <summary>
    /// Path to Screenline file.
    /// </summary>
    public string ScreenlineFileName
    {
        get
        {
            return _screenlineFileName;
        }
        set
        {
            _screenlineFileName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScreenlineFileName)));
        }
    }

    private string _textMessage = string.Empty;

    /// <summary>
    /// TextMessage property to display updates and error messages to the user.
    /// </summary>
    public string TextMessage
    {
        get
        {
            return _textMessage;
        }
        set
        {
            _textMessage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextMessage)));
        }
    }

    private string _textColour = "Black";

    /// <summary>
    /// Colour property of textblock to display when user is updated with progress. Green for success
    /// and red for errors.
    /// </summary>
    public string TextColour
    {
        get
        {
            return _textColour;
        }
        set
        {
            _textColour = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextColour)));
        }
    }


    private readonly ObservableCollection<VehicleCountTypeModel> _vehicleCountTypes;

    /// <summary>
    /// Collection of VehicleCountTypeModel objects to get a list of VehicleCountType objects.
    /// </summary>
    public ReadOnlyObservableCollection<VehicleCountTypeModel> VehicleCountTypes
        => new(_vehicleCountTypes);

    //private int _vehicleCountTypeId;
    ///// <summary>
    ///// Set the value of the user selected VehicleCountTypeId
    ///// </summary>
    //public int VehicleCountTypeId
    //{
    //    get
    //    {
    //        return _vehicleCountTypeId;
    //    }
    //    set
    //    {
    //        _vehicleCountTypeId = value;
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VehicleCountTypeId)));
    //    }
    //}

    //private int _occupancyNumber;
    ///// <summary>
    ///// Set the value of the user selected occupancy number.
    ///// </summary>
    //public int OccupancyNumber
    //{
    //    get
    //    {
    //        return _occupancyNumber;
    //    }
    //    set
    //    {
    //        _occupancyNumber = value;
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OccupancyNumber)));
    //    }
    //}

    //private int _countType = 1;
    ///// <summary>
    ///// Set the value of the user selected Vehcilecounttypeid
    ///// </summary>
    //public int CountType
    //{
    //    get
    //    {
    //        return _countType;
    //    }
    //    set
    //    {
    //        _countType = value;
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountType)));
    //    }
    //}

    //private string _vehicleDescription = string.Empty;

    ///// <summary>
    ///// Description of vehicle e.g. auto1.
    ///// </summary>
    //public string VehicleDescription
    //{
    //    get
    //    {
    //        return _vehicleDescription;
    //    }
    //    set
    //    {
    //        _vehicleDescription = value;
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VehicleDescription)));
    //    }
    //}

    //private string _vehicleName = string.Empty;

    ///// <summary>
    ///// Name of vehicle e.g. auto
    ///// </summary>
    //public string VehicleName
    //{
    //    get
    //    {
    //        return _vehicleName;
    //    }
    //    set
    //    {
    //        _vehicleName = value;
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VehicleName)));
    //    }
    //}

    /// <summary>
    /// Controls access to the CCDRS model repository.
    /// </summary>
    public CCDRSManagerViewModel()
    {
        Regions = _ccdrsRepository?.Regions
            ?? new(new ObservableCollection<RegionModel>());
        _vehicleCountTypes = _ccdrsRepository?.VehicleCountTypeModels
            ?? new(new ObservableCollection<VehicleCountTypeModel>());
    }

    /// <summary>
    /// Method to clear the clear all tracked entities before we begin any unit of work.
    /// </summary>
    public void ClearChangeTracker()
    {
        _ccdrsRepository.ClearChangeTracker();
    }

    /// <summary>
    /// Check if the survey exists in the database or not.
    /// </summary>
    public bool CheckSurveyExists()
    {
        return _ccdrsRepository.CheckSurveyExists(RegionId, SurveyYear);
    }

    /// <summary>
    /// Delete the survey data from the database for selected region and year.
    /// </summary>
    public void DeleteSurveyData()
    {
        _ccdrsRepository.DeleteSurveyData(RegionId, SurveyYear);
    }

    /// <summary>
    /// Add survey data to the database.
    /// </summary>
    public void AddSurveyData()
    {
        _ccdrsRepository.AddSurveyData(RegionId, SurveyYear);
    }

    /// <summary>
    /// Add station data to the database.
    /// </summary>
    public void AddStationData()
    {
        _ccdrsRepository.AddStationData(StationFileName, RegionId);
    }

    /// <summary>
    /// Add SurveyStation data to the database.
    /// </summary>
    public void AddSurveyStationData()
    {
        _ccdrsRepository.AddSurveyStationData(RegionId, SurveyYear);
    }

    /// <summary>
    /// Add StationCountObservation data to the database.
    /// </summary>
    public void AddStationCountObserationData()
    {
        _ccdrsRepository.AddStationCountObservationData(StationCountObservationFile, RegionId, SurveyYear);
    }

    /// <summary>
    /// Add Screenline data to the database.
    /// </summary>
    public void AddScreenlineData()
    {
        _ccdrsRepository.AddScreenlineData(RegionId, ScreenlineFileName);
    }

    /// <summary>
    /// Add the colour and text to the textblock data
    /// </summary>
    /// <param name="colour">The colour of the text e.g. red.</param>
    /// <param name="message">The message to display and write.</param>
    public void SetTextBlockData(string colour, string message)
    {
        TextColour = colour;
        TextMessage = message;
    }

    /// <summary>
    /// The Steps to run to add survey data to the database. 
    /// </summary>
    /// <returns></returns>
    public Task StepsToRunAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                // Turn on the progress bar.
                IsRunning = true;
                ClearChangeTracker();
                // If the StationFile is valid continue processing remaining steps.
                if (CheckStationFile())
                {
                    // If the stationcountobservation file is valid continue processing the steps.
                    if (CheckStationCountFile())
                    {
                        SetTextBlockData("green", "Deleting survey data if exists please wait...");
                        DeleteSurveyData();
                        SetTextBlockData("green", "Successfully deleted survey data.");
                        SetTextBlockData("green", "Attempting to add survey data to the database please wait...");
                        AddSurveyData();
                        SetTextBlockData("green", "Successfully added survey data");
                        SetTextBlockData("green", "Attempting to add Station Data please wait...");
                        AddStationData();
                        SetTextBlockData("green", "Successfully added station data");
                        SetTextBlockData("green", "Attempting to add surveystation data to the database please wait...");
                        AddSurveyStationData();
                        SetTextBlockData("green", "Successfully added surveystation data");
                        SetTextBlockData("green", "Attempting to add station Count observation data to the database please wait...");
                        AddStationCountObserationData();
                        SetTextBlockData("green", "Successfully added stationcount observation data");
                        SetTextBlockData("green", "Please click Finish button to close the application.");
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextBlockData("red", ex.Message);
            }
            finally
            {
                // Turn off the progress bar.
                IsRunning = false;
            }
        });
    }

    /// <summary>
    /// Add screenline data of a given region.
    /// </summary>
    /// <returns></returns>
    public Task AddScreenlineAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                // Turn on the progress bar.
                IsRunning = true;
                ClearChangeTracker();
                if (CheckScreenlineFile())
                {
                    // Screenline file is valid now updating the database.
                    SetTextBlockData("green", "Success now uploading Screenline data please wait...");
                    AddScreenlineData();
                    SetTextBlockData("green", "Screenline data successfully added. Click x to close the application");
                }
            }
            catch (Exception ex)
            {
                SetTextBlockData("red", ex.Message);
            }
            finally
            {
                // Turn off the progress bar.
                IsRunning = false;
            }
        });
    }

    /// <summary>
    /// Delete Survey data from the database. 
    /// This deletes data from the survey, station, stationcount 
    /// and surveystation tables in a cascade manner.
    /// </summary>
    /// <returns></returns>
    public Task DeleteSurveyDataAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                // Turn on the progress bar.
                IsRunning = true;
                ClearChangeTracker();
                SetTextBlockData("green", "Deleting the survey data please wait");
                DeleteSurveyData();
                SetTextBlockData("green", "Successfully deleted data please click x to close the application");
            }
            catch (Exception ex)
            {
                SetTextBlockData("red", ex.Message);
            }
            finally
            {
                // Turn off the progress bar.
                IsRunning = false;
            }
        });
    }

    private VehicleCountTypeModel? _selectedVehicleCountType;

    /// <summary>
    /// VehicleCountTypeModel property.
    /// </summary>
    public VehicleCountTypeModel? SelectedVehicleCountType
    {
        get
        {
            return _selectedVehicleCountType;
        }
        set
        {
            _selectedVehicleCountType = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedVehicleCountType)));
            SelectedVehicleCountTypeViewModel = new VehicleCountTypeViewModel(value);
        }
    }

    private VehicleCountTypeViewModel? _selectedVehicleCountTypeViewModel;

    /// <summary>
    /// Selected VehicleCountType View Model
    /// </summary>
    public VehicleCountTypeViewModel? SelectedVehicleCountTypeViewModel
    {
        get
        {
            return _selectedVehicleCountTypeViewModel;
        }
        set
        {
            _selectedVehicleCountTypeViewModel = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedVehicleCountTypeViewModel)));
        }
    }

    /// <summary>
    /// VehicleCountTypeViewModel to 
    /// </summary>
    public class VehicleCountTypeViewModel : INotifyPropertyChanged
    {

        public int Id { get; set; }

        public string _description;

        /// <summary>
        /// Description of VehicleCountType.
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        /// <summary>
        /// Number of occupants that can sit in the vehicle.
        /// </summary>
        private int _occupancy;
        public int Occupancy
        {
            get
            {
                return _occupancy;
            }
            set
            {
                _occupancy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Occupancy)));
            }
        }
        private int _countType;

        /// <summary>
        /// Stores the type of vehicle. Used to determine drop down options
        /// </summary>
        public int CountType
        {
            get
            {
                return _countType;
            }
            set
            {
                _countType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountType)));
            }
        }

        /// <summary>
        /// Vehicle Name.
        /// </summary>
        public string VehicleName => _model?.VehicleName ?? "no vehicle selected";

        private readonly VehicleCountTypeModel? _model;
        public VehicleCountTypeViewModel(VehicleCountTypeModel? model)
        {
            if (model is not null)
            {
                Id = model.Id;
                _description = model.Description;
                Occupancy = model.Occupancy;
                CountType = model.CountType;
                _model = model;
            }
            else
            {
                Id = 0;
                _description = string.Empty;
                Occupancy = 0;
                CountType = -1;
                _model = null;
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsUpdateable
        {
            get
            {
                return _model is not null;
            }
        }

        /// <summary>
        /// Update the model with the updated values.
        /// </summary>
        internal void Update()
        {
            if (_model is not null)
            {
                _model.Description = Description;
                _model.Occupancy = Occupancy;
                _model.CountType = CountType;
            }
        }
    }

    /// <summary>
    /// Update the VehicleCountType object with new data.
    /// </summary>
    public void UpdateVehicleData()
    {
        try
        {
            SetTextBlockData("green", "updating vehicle information please wait...");
            if (SelectedVehicleCountTypeViewModel is not null
                && SelectedVehicleCountType is not null)
            {
                SelectedVehicleCountTypeViewModel.Update();
                _ccdrsRepository.UpdateVehicleData(SelectedVehicleCountType);
            }
            SetTextBlockData("green", "Successfully updated Click x to close or continue");
        }
        catch (Exception ex)
        {
            SetTextBlockData("red", ex.Message);
        }
    }

    /// <summary>
    /// Check if the station file is valid before doing database operations.
    /// </summary>
    /// <returns>True if the validations succeeds false if the validation fails.</returns>
    public bool CheckStationFile()
    {
        SetTextBlockData("green", "Checking and validating Station file please wait...");
        bool isValid = CCDRSManagerModelRepository.ValidateStationFile(StationFileName, out var message);
        if (isValid)
        {
            SetTextBlockData("green", "Success no errors found in station file...");
            return true;
        }
        else
        {
            SetTextBlockData("red", message);
            return false;
        }
    }

    /// <summary>
    /// Check if the StationCountObservation file is valid before doing any database operations.
    /// </summary>
    public bool CheckStationCountFile()
    {
        SetTextBlockData("green", "Checking and validating StationCountObservation file please wait...");
        bool isValid = CCDRSManagerModelRepository.ValidateStationCountObservationFile(StationCountObservationFile, out string? message);
        if (isValid)
        {
            SetTextBlockData("green", "Success no errors found in stationcountobservation file...");
            return true;
        }
        else
        {
            SetTextBlockData("red", message);
            return false;
        }
    }

    /// <summary>
    /// Check if the Screenline file is valid before doing any database operations.
    /// </summary>
    public bool CheckScreenlineFile()
    {
        SetTextBlockData("green", "Checking Screenline file for errors...");
        bool isValid = CCDRSManagerModelRepository.ValidateScreenlineFile(ScreenlineFileName, out var message);
        if (isValid)
        {
            SetTextBlockData("green", "Success no errors found in screenline file...");
            return true;
        }
        else
        {
            SetTextBlockData("red", message);
            return false;
        }
    }
}
