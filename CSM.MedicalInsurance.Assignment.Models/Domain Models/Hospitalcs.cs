using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.MedicalInsurance.Assignment.Models.Domain_Models
{
    public class Hospital
    {
        public string Name { get; set; }
        public string Area { get; set;}
        public string Street { get; set;}   
        public string City { get; set;} 
        public string PinCode { get; set;}  
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set;}
       
    }
}
