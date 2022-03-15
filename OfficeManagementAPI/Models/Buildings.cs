using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeManagementAPI.Models
{
    public class Buildings
    {
        public int BuildingID { get; set; }
        public string BuildingName { get; set; }
        public int FloorsNo { get; set; }
        public string BuildingAddress { get; set; }
    }
}
