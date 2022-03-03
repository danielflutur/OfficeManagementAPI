using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeManagementAPI.Models
{
    public class Building
    {
        public string BuildingName { get; set; }
        public int NumberOfFloors { get; set; }
        public string BuildingAddress { get; set; }
    }
}
