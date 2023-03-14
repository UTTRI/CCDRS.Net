using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

///
/// Utility file to check and validate txt and csv files are valid before doing database operations.
///
namespace CCDRSManager;

public class FileUtility
{
    /// <summary>
    /// Check if the station file uploaded by user is valid. Checks for various errors and throws custom
    /// exceptions back to user.
    /// </summary>
    /// <param name="stationFileName">FilePath to station file.</param>
    /// <exception cref="Exception"></exception>
    public void CheckStationFileForErrors(string stationFileName)
    {
        Console.WriteLine("this ran");
        // Loop through the station csv file
        using (var readFile = new StreamReader(stationFileName))
        {
            string? line;
            string[] row;
            List<string> stationCodeList = new() { };
            readFile.ReadLine();
            int lineNumber = 0;

            // loop through the line
            while ((line = readFile.ReadLine()) is not null)
            {
                lineNumber++;
                row = line.Split(';');
                string stationCode = row[0];

                //the parse doesn't show 2 columns
                if (row.Length != 2)
                {
                    throw new Exception($"Corrupt data in file {stationFileName} on line {lineNumber} couldn't parse the line \n");
                }

                // station is null or not supplied
                if (stationCode == "")
                {
                    throw new Exception($"Missing code {stationFileName} on line {lineNumber} Please Reupload \n");
                }

                //Duplicate data exists
                if (stationCodeList.Contains(stationCode))
                {
                    throw new Exception($"Duplicate data found on line {lineNumber} Please Reupload \n");
                }
                else
                {
                    stationCodeList.Add(stationCode);
                }
            }
        }
    }

    /// <summary>
    /// Check if the Screenline file uploaded by user is valid. Checks for various errors and throws custom
    /// exceptions back to user.
    /// </summary>
    /// <param name="screenlineFileName">File path to the screenline file.</param>
    /// <exception cref="Exception"></exception>
    public void CheckScreenlineFile(string screenlineFileName)
    {
        // read the screenline csv file
        using var readFile = new StreamReader(screenlineFileName);
        string? line;
        string[] rowData;
        List<string> stationCodeList = new() { };
        int lineNumber = 0;

        // Loop through the remaining rows of data and insert the screenline data into the database.
        while ((line = readFile.ReadLine()) is not null)
        {
            lineNumber++;
            rowData = line.Split(',');

            //the parse doesn't show 3 columns
            if (rowData.Length != 3)
            {
                throw new Exception($"Corrupt data in file {screenlineFileName} on line {lineNumber} couldn't parse the line \n");
            }
            else
            {
                string slineCode = rowData[0];
                string slineDescription = rowData[1];
                string stationCode = rowData[2];

                // Screenline is null or not supplied
                if (stationCode == "")
                {
                    throw new Exception($"Missing station code {screenlineFileName} on line {lineNumber} Please Reupload \n");
                }

                // Duplicate data exists
                if (stationCodeList.Contains(stationCode))
                {
                    throw new Exception($"Duplicate station code in file {screenlineFileName} found on line {lineNumber} Please Reupload \n");
                }
                else
                {
                    stationCodeList.Add(stationCode);
                }
            }
        }
    }

    /// <summary>
    /// Check if StationCountObservation file is valid.
    /// </summary>
    /// <param name="stationCountObservationFile">File path to StationCountObservation file.</param>
    /// <exception cref="Exception"></exception>
    public void CheckStationCountObservationFile(string stationCountObservationFile)
    {
        // extract the header of the ccdrs file.
        string[] headerLine;
        // read the station_count_observation ccdrs csv file
        using (var readFile = new StreamReader(stationCountObservationFile))
        {
            int lineNumber = 0;
            string? line;
            string[] rowData;

            List<Tuple<string, int>> stationCountTupleList = new() { };

            // Extract the header of the csv file which contains the columns of vehicle types.
            headerLine = readFile.ReadLine()?.Split(',') ?? throw new Exception("No header file found");

            // Loop through the remaining rows of data and insert the observation data into the database.
            while ((line = readFile.ReadLine()) is not null)
            {

                lineNumber++;
                rowData = line.Split(',');
                string stationCode = rowData[0];
                int time = int.Parse(rowData[1]);
                bool exists = stationCountTupleList.Any(t => t.Item1 == stationCode && t.Item2 == time);

                // Check if any duplicate stationcode and time exists.
                if (exists)
                {
                    throw new Exception($"Duplicate station code in file {stationCountObservationFile} found on line {lineNumber}. Fix and please reupload");
                }
                else
                {
                    stationCountTupleList.Add(new Tuple<string, int>(stationCode, time));
                }

                //check if any empty cells exists.
                bool emptyStringExists = rowData.Any(s => string.IsNullOrEmpty(s));
                if (emptyStringExists)
                {
                    throw new Exception($"Empty data exists in file {stationCountObservationFile} found on line {lineNumber}");
                }
            }
        }
    }
}
