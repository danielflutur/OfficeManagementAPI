﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeManagementAPI.Models
{
    public class Employees
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Passw { get; set; }
        public string EmpRole { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string EmpStatus { get; set; }
        public int DeskNr { get; set; }
        public string OfficeName { get; set; }
        public int FloorNr { get; set; }
        public string BuildingName { get; set; }
    }
}
