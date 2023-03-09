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

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

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
    /// Name of StationCountObservation CCDRS csv file.
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
    /// Name of screenline csv file.
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

    /// <summary>
    /// Controls access to the CCDRS model repository.
    /// </summary>
    public CCDRSManagerViewModel()
    {
        Regions = _ccdrsRepository.Regions;
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
                SetTextBlockData("green", "Uploading Screenline data please wait...");
                AddScreenlineData();
                SetTextBlockData("green", "Screenline data successfully added. Click x to close the application");
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
    /// This deletes data from the survey, station, stationcount and surveystation tables in a cascade manner.
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
}
