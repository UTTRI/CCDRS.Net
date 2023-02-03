/*
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
using CCDRSManager.Data;
using CCDRSManager.Model;

namespace CCDRSManager;

/// <summary>
/// ViewModel Class to manage and track property changes 
/// </summary>
public class CCDRSManagerViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Observable List of all regions that exist in the database.
    /// </summary>
    public ReadOnlyObservableCollection<RegionModel> Regions { get; }

    /// <summary>
    /// Property of the selected region username received from region combobox.
    /// </summary>
    private int selectedRegionId { get; set; }
    /// <summary>
    /// Property of selected year of survey received from survey textbox.
    /// </summary>
    public int selectedSurvey { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Set the value of the user selected RegionId
    /// </summary>
    public int SelectedRegionId
    {
        get
        {
            return selectedRegionId;
        }
        set
        {
            selectedRegionId = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedRegionId)));
        }
    }

    /// <summary>
    /// Set the value of the user selected Survey year.
    /// </summary>
    public int SelectedSurvey
    {
        get
        {
            return selectedSurvey;
        }
        set
        {
            selectedSurvey = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedSurvey)));
        }
    }

    // Initialize the class constructor
    public CCDRSManagerViewModel() 
    {
        CCDRSManagerModelRepository ccdrsRepository = App.Us.CCDRSManagerModelRepository;
        Regions = ccdrsRepository.Regions;
    }
    /// <summary>
    /// Method to check if the survey exists in the database or not.
    /// </summary>
    public bool CheckSurvey()
    {
        CCDRSManagerModelRepository ccdrsRepository = App.Us.CCDRSManagerModelRepository;
        return App.Us.CCDRSManagerModelRepository.CheckSurvey(SelectedRegionId, selectedSurvey);
    }

}
