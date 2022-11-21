using CCDRS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace CCDRS.Pages
{
    /// <summary>
    /// Class to display the data for the Specific Screenline page.
    /// </summary>
    public class SpecificScreenlineModel : PageModel
    {
        private readonly CCDRS.Data.CCDRSContext _context;

        public SpecificScreenlineModel(CCDRS.Data.CCDRSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Initialize dropdown list of directions used to store user selected directions.
        /// </summary>
        [BindProperty]
        public IList<Direction> DirectionList { get; set; }

        /// <summary>
        /// Initialize list of vehicle count types used to store user selected vehicle count options.
        /// </summary>
        [BindProperty]
        public IList<IndividualCategory> VehicleCountTypeList { get; set; } = default!;

        /// <summary>
        /// Initialize list of person count types used to store user selected person count options.
        /// </summary>
        [BindProperty]
        public IList<IndividualCategory> PersonCountTypeList { get; set; } = default!;

        /// <summary>
        /// Initialize list of technologies available used to store user selected technologies
        /// </summary>
        [BindProperty]
        public IList<IndividualCategory> IndividualCategoriesList { get; set; } = default!;

        /// <summary>
        /// Initialize a select list of all screenlines available for a given region.
        /// </summary>
        public IEnumerable<SelectListItem> SelectedScreenlines { get; set; } = default!;

        /// <summary>
        /// Set a global default variable to save the variable id this reads the value from the url
        /// The primary key of the selected region
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int RegionId { get; set; }

        /// <summary>
        /// The user selected survey id read from the url.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public int SelectedSurveyId { get; set; }

        /// <summary>
        /// Display the data on page load
        /// </summary>
        /// <returns>The html page with the populated data</returns>
        public async Task OnGetAsync()
        {
            // Query region table to display the region name to the front end.
            var regionName = _context.Regions
                              .Where(r => r.Id == RegionId)
                              .SingleOrDefault();

            // Bind the local variable to the ViewData to display to the front-end
            ViewData["RegionName"] = regionName?.Name;

            // Query survey table to display the survey year to the front end.
            var surveyYear = _context.Surveys
                              .Where(s => s.Id == SelectedSurveyId)
                              .SingleOrDefault();

            // Bind the local variable to the ViewData to display to the front-end
            ViewData["SurveyYear"] = surveyYear?.Year;

            // Query screenline table to return a list of all screenline associated with a given region.
            if (_context.Screenlines is not null)
            {
                SelectedScreenlines = new SelectList((from screenLine in _context.Screenlines
                                                        where screenLine.RegionId == RegionId
                                                        orderby screenLine.SlineCode
                                                        select new
                                                        {
                                                            Id = screenLine.Id,
                                                            Title = screenLine.SlineCode + " " + screenLine.Note
                                                        }
                                                            ), "Id", "Title");
            }

            // Query directions table to return direction radiobutton options.
            if (_context.Directions != null)
            {
                DirectionList = Utility.Directions;
            }

            // Query individual_categories table to return various radiobutton options.
            if (_context.IndividualCategories != null)
            {
                // List all total vehicle count options
                VehicleCountTypeList = Utility.GetTotalVehicleCounts(RegionId, SelectedSurveyId);

                // List all total person count options
                PersonCountTypeList = Utility.GetTotalPersonCounts(RegionId, SelectedSurveyId);

                // List all technologies options
                IndividualCategoriesList = Utility.GetTechnologyCounts(RegionId, SelectedSurveyId);
            }
        }

        /// <summary>
        /// Post method that executes database query to generate plain/txt format output
        /// </summary>
        /// <returns>Redirects user to a text file page with the results from executed query.</returns>
        public IActionResult OnPostSubmit(char[] directionCountSelect,
            int startTime, int endTime, int SelectedScreenlineId,
            int trafficVolumeRadioButtonSelect,
            int[] individualCategorySelect, IList<IndividualCategory> IndividualCategoriesList
            )
        {
            // User selects total volume.
            if (trafficVolumeRadioButtonSelect == 1)
            {
                string x = GetTotalVolume(directionCountSelect, startTime,
                    endTime, SelectedScreenlineId,
                 individualCategorySelect, IndividualCategoriesList
                 );
                return Content(x);
            }
            else
            {
                // User selects fifteen minute interval.
                string x = GetFifteenMinuteInterval(directionCountSelect, startTime,
                    endTime, SelectedScreenlineId,
                 individualCategorySelect, IndividualCategoriesList
                 );
                return Content(x);
            }
        }

        /// <summary>
        /// Method to calculate the Fifteen Minute Interval results.
        /// </summary>
        /// <param name="startTime">Starting time user requested which is the start of the range</param>
        /// <param name="endTime">Ending time user requested which is the end of the range</param>
        /// <param name="SelectedScreenlineId">User selected Screenline</param>
        /// <param name="individualCategorySelect">List of all categories selected by user</param>
        /// <param name="individualCategoriesList">Default list of all categories available for selected survey</param>
        /// <returns>String representation of the results</returns>
        internal string GetFifteenMinuteInterval(char[] directionCountSelect, 
            int startTime, int endTime, int SelectedScreenlineId,
            int[] individualCategorySelect, IList<IndividualCategory> individualCategoriesList
        )
        {
            // Dictionary of station_count records with a key of the tuple of screenline name, time, direction and an array of observations. 
            Dictionary<(string screenLineName, int time, char direction), int[]> newlist = new();

            // Executes query and returns all station count data
            var dataList = ( from stationcount in _context.StationCountObservations
                             join surveystation in _context.SurveyStations on stationcount.SurveyStationId equals surveystation.Id
                             join vehicleCountType in _context.VehicleCountTypes on stationcount.VehicleCountTypeId equals vehicleCountType.Id
                             join vehicle in _context.Vehicles on vehicleCountType.VehicleId equals vehicle.Id
                             join station in _context.Stations on surveystation.StationId equals station.Id
                             join survey in _context.Surveys on surveystation.SurveyId equals survey.Id
                             join screenlinestation in _context.Screenlinestations on station.Id equals screenlinestation.StationId
                             join screenline in _context.Screenlines on screenlinestation.ScreenlineId equals screenline.Id
                             where screenline.Id == SelectedScreenlineId
                                && individualCategorySelect.Contains(vehicleCountType.Id)
                                && stationcount.Time >= startTime & stationcount.Time <= endTime
                                && directionCountSelect.Contains((char)station.Direction!)
                                && screenline.RegionId == RegionId
                                && survey.Id == SelectedSurveyId
                             group new {stationcount, screenline, station, vehicleCountType} by new { stationcount.Time, screenline.SlineCode, station.Direction, vehicleCountType.Id }
                             into newgrp
                             select new
                             {
                                 slineCode = newgrp.Key.SlineCode, 
                                 time = newgrp.Key.Time,
                                 direction = newgrp.Key.Direction,
                                 Observations = newgrp.Sum(x => x.stationcount.Observation),
                                 npFKId = newgrp.Key.Id,
                             }
                          );
            foreach (var item in dataList)
            {
                if (!newlist.TryGetValue((item.slineCode, item.time, item.direction), out var counts))
                {
                    newlist[(item.slineCode, item.time, item.direction)] = counts = new int[individualCategorySelect.Length];
                }
                counts[Array.IndexOf(individualCategorySelect, item.npFKId)] += item.Observations;
            }

            var builder = new StringBuilder();
            // Build the header
            builder.Append("Sline,Time");
            foreach (var item in individualCategorySelect)
            {
                var category = Utility.TechnologyNames.First(c => c.id == item);
                builder.Append("," + category.name);
            }
            builder.AppendLine();

            foreach (var item in from x in newlist.Keys
                                 orderby x.screenLineName, x.time
                                 select x
                                     )
            {
                var row = newlist[item];
                builder.Append(item.screenLineName);
                builder.Append(',');
                builder.Append(item.direction);
                builder.Append(',');
                builder.Append(item.time);
                foreach (var x in row)
                {
                    builder.Append(',');
                    builder.Append(x);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        /// <summary>
        /// Method to calculate the Total Volume results.
        /// </summary>
        /// <param name="startTime">Starting time user requested which is the start of the range</param>
        /// <param name="endTime">Ending time user requested which is the end of the range</param>
        /// <param name="SelectedScreenlineId">User selected Screenline</param>
        /// <param name="individualCategorySelect">List of all categories selected by user</param>
        /// <param name="IndividualCategoriesList">Default list of all categories available for selected survey</param>
        /// <returns>String representation of the results</returns>
        internal string GetTotalVolume(char[] directionCountSelect,
            int startTime, int endTime, int SelectedScreenlineId,
            int[] individualCategorySelect, IList<IndividualCategory> individualCategoriesList
            )
        {
            // Dictionary of station_count records with a key of the tuple of screenline and direction and an array of observations. 
            Dictionary<(string screenlinename, char direction), int[]> newlist = new();

            //outputs a list of all stationcount data for all technologies
            var datalist = (from stationcount in _context.StationCountObservations
                            join stationsurvey in _context.SurveyStations on stationcount.SurveyStationId equals stationsurvey.Id
                            join vehiclecount in _context.VehicleCountTypes on stationcount.VehicleCountTypeId equals vehiclecount.Id
                            join vehicle in _context.Vehicles on vehiclecount.VehicleId equals vehicle.Id
                            join survey in _context.Surveys on stationsurvey.SurveyId equals survey.Id
                            join station in _context.Stations on stationsurvey.StationId equals station.Id
                            join screenlinestation in _context.Screenlinestations on station.Id equals screenlinestation.StationId
                            join screenline in _context.Screenlines on screenlinestation.ScreenlineId equals screenline.Id
                            where
                                screenline.RegionId == RegionId
                                && survey.Id == SelectedSurveyId
                                && directionCountSelect.Contains((char)station.Direction!)
                                && screenline.Id == SelectedScreenlineId
                                && stationcount.Time >= startTime & stationcount.Time <= endTime
                                && individualCategorySelect.Contains(vehiclecount.Id)
                            group new { screenline, stationcount, vehicle, vehiclecount, station }
                            by new { screenline.SlineCode, vehicle.Name, vehiclecount.Occupancy, vehiclecount.Id, station.Direction }
                            into grp
                            select new
                            {
                                slinecode = grp.Key.SlineCode,
                                observations = grp.Sum(x => x.stationcount.Observation),
                                npfkid = grp.Key.Id,
                                direction = grp.Key.Direction
                            }
                      );
            foreach (var item in datalist)
            {
                if (!newlist.TryGetValue((item.slinecode, item.direction), out var counts))
                {
                    newlist[(item.slinecode, item.direction)] = counts = new int[individualCategorySelect.Length];
                }
                counts[Array.IndexOf(individualCategorySelect, item.npfkid)] += item.observations;
            }

            var builder = new StringBuilder();
            // Build the header
            builder.Append("sline,direction,startTime,endTime");
            foreach (var item in individualCategorySelect)
            {
                var category = Utility.TechnologyNames.First(c => c.id == item);
                builder.Append("," + category.name);
            }
            builder.AppendLine();

            foreach (var item in from x in newlist.Keys
                                 orderby x.screenlinename
                                 select x
                                 )
            {
                var row = newlist[item];
                builder.Append(item.screenlinename);
                builder.Append(',');
                builder.Append(item.direction);
                builder.Append(',');
                builder.Append(startTime);
                builder.Append(',');
                builder.Append(endTime);
                foreach (var x in row)
                {
                    builder.Append(',');
                    builder.Append(x);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
