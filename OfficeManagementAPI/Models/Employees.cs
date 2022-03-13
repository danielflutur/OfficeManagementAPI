using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace OfficeManagementAPI.Models
{
   
    public class Employees
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Passw { get; set; }
        public string EmpRole { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string EmpStatus { get; set; }
        public int DeskNo { get; set; }
        public string OfficeName { get; set; }
        public int FloorNo { get; set; }
        public string BuildingName { get; set; }
        public string WorkRemote { get; set; }
       
        public string Token { get; set; }
    }
    
}
