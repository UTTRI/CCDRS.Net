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

using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace CCDRSManager
{
    /// <summary>
    /// Interaction logic for CCDRSWizard.xaml
    /// </summary>
    public partial class CCDRSWizard : Window
    {
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
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Method to check if the survey data exists in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSurvey(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("this ran");
            bool value = (bool)((DataContext as CCDRSManagerViewModel)?.CheckSurvey());
            if (value)
            {
                MessageBox.Show("This data already exists in the database. We will delete all records " +
                    "Click yes to delete all records and no to cancel this operation",
                    "My App", MessageBoxButton.YesNo);
            }
            else
            {
                MessageBoxResult mClicked = MessageBox.Show("No duplicate survey data was discovered in the database " + 
                    "Click next to add station data", "My App");
            }
        }
    }
}
