using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeManagementAPI.Models
{
    public class Building
    {
        public string BuildingName { get; set; }
        public int NrOfFloors { get; set; }
        public string Address { get; set; }
    }
}
