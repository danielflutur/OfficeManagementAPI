using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeManagementAPI.Models
{
    public class Office
    {
        public string OfficeName { get; set; }
        public string BuildingName { get; set; }
        public int FloorNr { get; set; }
        public int NrOfDesks { get; set; }
        public int NrOfFreeDesks { get; set; }
        public string OfficeAdminName { get; set; }
    }
}
