using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeManagementAPI.Models
{
    public class Desks
    {
        public int DeskID { get; set; }
        public string EmployeeName { get; set; }
        public int DeskNo { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAdmin { get; set; }
        public string BuildingName { get; set; }
    }
}
