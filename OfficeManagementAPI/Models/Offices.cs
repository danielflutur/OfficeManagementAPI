using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeManagementAPI.Models
{
    public class Offices
    {
        public int OfficeID { get; set; }
        public string OfficeName { get; set; }
        public string BuildingName { get; set; }
        public int FloorNo { get; set; }
        public string OfficeAdminName { get; set; }
        public int TotalDesksCount { get; set; }
        public int UsableDesksCount { get; set; }
        public string OccupiedDesksCount { get; set; }
    }
}
