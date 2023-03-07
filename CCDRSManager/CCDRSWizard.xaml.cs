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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CCDRSManager;

/// <summary>
/// Interaction logic for CCDRSWizard.xaml
/// </summary>
public partial class CCDRSWizard : Window
{
    [GeneratedRegex("[^0-9]+")]
    private static partial Regex CheckNumber();

    public CCDRSWizard()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Method to ensure that the values entered in the survey textbox are only integer numbers.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = CheckNumber().IsMatch(e.Text);
    }

    /// <summary>
    /// Method to check if the survey data exists in the database.
    /// </summary>
    /// <param name="vm">CCDRSManagerViewModel</param>
    /// <returns>return true if data successfully uploaded.</returns>
    private async Task<bool> CheckSurveyExists(CCDRSManagerViewModel vm)
    {
        if (vm.CheckSurveyExists())
        {
            // If the user clicks the yes button on the message box delete the survey data.
            System.Windows.MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(this, "This data already exists in the database. We will delete all records " +
           "Click yes to delete all records and no to cancel this operation", "", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // Delete the survey
                await Task.Run(() => { vm.DeleteSurveyData(); });
            }
            else
            {
                // If the user doesn't want to continue abort.
                return false;
            }
        }
        else
        {
            System.Windows.MessageBox.Show(this, "No duplicate survey data was discovered in the database " +
                "Click next to add station data");
        }
        return true;
    }

    /// <summary>
    /// Generic method to open a window dialog box to upload a file. 
    /// </summary>
    /// <param name="nameOfTextbox">Name of Textbox control.</param>
    public void OpenFileDialog(TextBox nameOfTextbox)
    {
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.OpenFileDialog();
        // Show open file dialog box
        bool? result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            // Open document
            string filename = dialog.FileName;
            nameOfTextbox.Text = filename;
        }
    }

    /// <summary>
    /// Open the station file dialog window to upload a station csv file.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenStationFileDialog(object sender, RoutedEventArgs e)
    {
        OpenFileDialog(StationFileName);
    }

    /// <summary>
    /// Open the stationcount observation window dialog window to select stationcount observation file.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenStationCountFile(object sender, RoutedEventArgs e)
    {
        OpenFileDialog(StationCountObservationFile);
    }

    /// <summary>
    /// Add the Station Data to the database.
    /// </summary>
    /// <param name="vm">CCDRSManagerViewModel</param>
    /// <returns>return true if data successfully uploaded.</returns>
    private async Task<bool> AddStationData(CCDRSManagerViewModel vm)
    {
        await Task.Run(() =>
        {
            vm.AddSurveyData();
            vm.AddStationData();
            vm.AddSurveyStationData();
        });
        return true;
    }

    /// <summary>
    /// Add StationCountObservation data to the database.
    /// </summary>
    /// <param name="vm">CCDRSManagerViewModel</param>
    /// <returns>return true if data successfully uploaded.</returns>
    private async Task<bool> AddStationCountObservationData(CCDRSManagerViewModel vm)
    {
        await Task.Run(() => { vm.AddStationCountObserationData(); });
        System.Windows.MessageBox.Show(this, "Successfully added station count observation data to the database. Press Finish and close the wizard.");
        return true;
    }

    /// <summary>
    /// Submit button which runs all database operations.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Submit(object sender, RoutedEventArgs e)
    {
        if (DataContext is CCDRSManagerViewModel vm)
        {
            // Disable the next, previous and finish buttons.
            Page3.CanSelectPreviousPage = false;
            Page3.CanSelectNextPage = false;
            Page3.CanFinish = false;

            //Run the progress bar.
            vm.IsRunning = true;
            try
            {
                await CheckSurveyExists(vm);
                await AddStationData(vm);
                await AddStationCountObservationData(vm);
            }
            catch (Exception ex)
            {
                vm.TextBlockData("red", ex.Message);
            }


            // close the progress bar.
            vm.IsRunning = false;

            // Activate the finish button for user to close the wizard.
            Page3.CanFinish = true;

        }
    }
}