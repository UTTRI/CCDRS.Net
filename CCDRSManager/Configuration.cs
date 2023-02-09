using CCDRSManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDRSManager;

public static class Configuration
{
    private static readonly CCDRSContext _context = new CCDRSContext();
    public static CCDRSManagerModelRepository CCDRSManagerModelRepository { get; private set; }

    public static void Initialize()
    {
        CCDRSManagerModelRepository = new CCDRSManagerModelRepository(_context);
    }
}
