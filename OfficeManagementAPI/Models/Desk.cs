﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeManagementAPI.Models
{
    public class Desk
    {
        public int DeskID { get; set; }
        public int DeskNr { get; set; }
        public string DeskStatus { get; set; }
        public string OfficeName { get; set; }
        public int EmployeeID { get; set; }
    }
}
