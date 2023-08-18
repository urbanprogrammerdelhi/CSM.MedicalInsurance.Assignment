using System;
using System.Collections.Generic;

namespace CSM.MedicalInsurance.Assignment.Models
{
   
    public partial class Insurance
    {
        public int InsuranceId { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string InsuranceContactEmail { get; set; }
    }

//    InsuranceCompanyName InsuranceContactEmail   CoverPercentage Scheme  DateOfInsurance
//ICICI Lombard bishnuprath@yahoo.com	50.00	Silver	2023-03-04
}
