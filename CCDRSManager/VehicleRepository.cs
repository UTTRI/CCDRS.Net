using CCDRSManager.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CCDRSManager;

public class VehicleRepository
{
    private readonly CCDRSContext _context;
    private readonly ObservableCollection<VehicleCountTypeModel> _vehiclesModel;

    public VehicleRepository(CCDRSContext context)
    {
        _context = context;
         _vehiclesModel = new ObservableCollection<VehicleCountTypeModel>(_context.VehicleCountTypes.Select(r => new VehicleCountTypeModel(r)));
    }

    /// <summary>
    /// Method to get a list of all vehicle_count_types that exist in the database.
    /// </summary>
    public ObservableCollection<VehicleCountTypeModel> Vehicles
    {
        get => new(_vehiclesModel);
    }

    public void UpdateVehicleCountTypeData(int vehicleCountTypeId, string header, string newValue)
    {
        var item = _context.VehicleCountTypes.Find(vehicleCountTypeId);
        if (header == "Description")
        {
            item.GetType().GetProperty(header).SetValue(item, newValue);
        }
        else
        {
            item.GetType().GetProperty(header).SetValue(item, int.Parse(newValue));
        }
        
        _context.SaveChanges();

    }
}
