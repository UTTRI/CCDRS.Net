/*
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

using CCDRS.Data;

namespace CCDRS.Model;

/// <summary>
/// Class that maps to the individual_categories view.
/// </summary>
public sealed class IndividualCategory
{
    public IndividualCategory()
    {

    }

    /// <summary>
    /// primary key of individual category table. 
    /// It is needed for the DbSet in the contextservice to work.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The number of occupants that can sit in a vehicle.
    /// </summary>
    public int OccupancyId { get; set; }

    /// <summary>
    /// Foreign key to the vehicle primary key.
    /// </summary>
    public int VehicleId { get; set; }

    /// <summary>
    /// Description of category to display to front end of user.
    /// </summary>
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// The hierarchy order of which the individual categories will be displayed to the user.
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// The type of category if it is a total vehicle, total person or others. 
    /// </summary>
    public int CountType { get; set; }


    /// <summary>
    /// The name of the Vehicle
    /// </summary>
    public string VehicleName { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key to the vehicle_count_type table associated to the vehicle_count_type primary key attribute.
    /// </summary>
    public int VehicleCountTypeId { get; set; }

    enum TechnologyCount
    {
        TechnologyCounts = 1, // Filter all remaining technologies not the two below
        VehicleCounts = 2, // Filter the total vehicle counts 
        PersonCounts = 3 // Filter the total person counts 
    }

    /// <summary>
    /// Get the list of Individual categories that exist for a given survey.
    /// </summary>
    /// <param name="selectedSurveyId">The surveyId to filter the subset of the technologies available.</param>
    /// <param name="_context">The context service to access the database tables.</param>
    /// <returns></returns>
    public static IList<IndividualCategory> GetIndividualCategoriesBasedOnSurvey(int selectedSurveyId, CCDRSContext _context)
    {
        return (from vehiclecounttypes in _context.VehicleCountTypes
                        join stationcountobserverations in _context.StationCountObservations on vehiclecounttypes.Id equals stationcountobserverations.VehicleCountTypeId
                        join surveystations in _context.SurveyStations on stationcountobserverations.SurveyStationId equals surveystations.Id
                        join surveys in _context.Surveys on surveystations.SurveyId equals surveys.Id
                        join vehicles in _context.Vehicles on vehiclecounttypes.VehicleId equals vehicles.Id
                        where
                            surveys.Id == selectedSurveyId
                        select new IndividualCategory()
                        {
                            OccupancyId = vehiclecounttypes.Occupancy,
                            VehicleId = vehiclecounttypes.VehicleId,
                            Description = vehiclecounttypes.Description,
                            DisplayOrder = vehicles.DisplayOrder,
                            CountType = vehiclecounttypes.CountType,
                            VehicleName = vehicles.Name,
                            VehicleCountTypeId = vehiclecounttypes.Id
                        }).Distinct().OrderBy(s => s.DisplayOrder).ThenBy(s => s.OccupancyId).ToList();
    }

    /// <summary>
    /// General method to filter the individual category list based on the technology count type used to display
    /// subset of categories in appropriate section to user.
    /// </summary>
    /// <param name="technologies">List of selected technologies for selected survey</param>
    /// <param name="countType"></param>
    /// <returns></returns>
    private static IList<IndividualCategory> GetSpecificTechnologies(IList<IndividualCategory> technologies, int countType)
    {
        return technologies.Where(s => s.CountType == countType).ToList();
    }

    /// <summary>
    /// Method to return the total vehicle count technologies for selected survey. 
    /// </summary>
    /// <param name="technologies">List of selected technologies for selected survey</param>
    /// <returns>A list of the individual categories of total vehicles.</returns>
    public static IList<IndividualCategory> GetTotalVehicleCounts(IList<IndividualCategory> technologies)
    {
        return GetSpecificTechnologies(technologies, (int)TechnologyCount.VehicleCounts);
    }

    /// <summary>
    /// Method to return the total person count technologies for selected survey.
    /// </summary>
    /// <param name="technologies">List of selected technologies for selected survey</param>
    /// <returns></returns>
    public static IList<IndividualCategory> GetTotalPersonCounts(IList<IndividualCategory> technologies)
    {
        return GetSpecificTechnologies(technologies, (int)TechnologyCount.PersonCounts);
    }

    /// <summary>
    /// Method to return the technologies available for selected survey.
    /// </summary>
    /// <param name="technologies">List of selected technologies for selected survey</param>
    /// <returns></returns>
    public static IList<IndividualCategory> GetTechnologyCounts(IList<IndividualCategory> technologies)
    {
        return GetSpecificTechnologies(technologies, (int)TechnologyCount.TechnologyCounts);
    }
}
