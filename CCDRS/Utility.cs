using CCDRS.Model;
using Microsoft.AspNetCore.Builder;

namespace CCDRS
{
    public static class Utility
    {
        public class GetNumofPersonTechnologyData
        {
            private readonly CCDRS.Data.CCDRSContext _context;
            public GetNumofPersonTechnologyData(CCDRS.Data.CCDRSContext context)
            {
                _context = context;
            }
            public List<(int id, string name)> GenerateNumofPersonTechIdList()
            {
                return (from t in _context.Vehicles
                        join np in _context.VehicleCountTypes on t.Id equals np.VehicleId
                        select new
                        {
                            Id = np.Id,
                            Name = t.Name + np.Occupancy
                        })
                       .ToList()
                       .Select(x => (x.Id, x.Name))
                       .ToList();
            }
        }
        public static List<(int id, string name)> NumofPersonTechIdList;
        public static void Initialize(CCDRS.Data.CCDRSContext context)
        {
            GetNumofPersonTechnologyData ok = new GetNumofPersonTechnologyData(context);
            NumofPersonTechIdList = ok.GenerateNumofPersonTechIdList();
        }
    }
}
