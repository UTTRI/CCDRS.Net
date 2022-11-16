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

using CCDRS.Model;
using Microsoft.AspNetCore.Builder;

namespace CCDRS
{
    /// <summary>
    /// Contains cached data. Call Initialize before usage.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Private method to generate a list of vehicles and associated occupancy. 
        /// </summary>
        /// <param name="context">The CCDRS context service</param>
        /// <returns>A list of the query with the id and name and occupancy</returns>
        private static List<(int id, string name)> GenerateNumofPersonTechIdList(CCDRS.Data.CCDRSContext context)
        {
            return (from t in context.Vehicles
                    join np in context.VehicleCountTypes on t.Id equals np.VehicleId
                    select new
                    {
                        Id = np.Id,
                        Name = t.Name + np.Occupancy
                    })
                   .ToList()
                   .Select(x => (x.Id, x.Name))
                   .ToList();
        }

        public static List<(int id, string name)> NumofPersonTechIdList;

        /// <summary>
        /// Invoke this method to initialize cached data.
        /// </summary>
        /// <param name="context">The CCDRS context service</param>
        public static void Initialize(CCDRS.Data.CCDRSContext context)
        {
            NumofPersonTechIdList = GenerateNumofPersonTechIdList(context);
        }
    }
}
