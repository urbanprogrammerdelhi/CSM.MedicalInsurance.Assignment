using CSM.MedicalInsurance.Assignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSM.MedicalInsurance.Assignment.Models
{

    public partial class PatientInsurance
    {
        public int PatientInsuranceId { get; set; }
        public int InsuranceId { get; set; }
        [DisplayName("Company Name")]
        public string InsuranceCompanyName { get; set; }
        public string InsuranceContactEmail { get; set; }
        [DisplayName("Cover Percentage")]

        public string CoverPercentage { get; set; }
        [DisplayName("Date of Insurance")]

        public string DateOfInsurance { get; set; }
        [DisplayName("Scheme")]

        public string Scheme { get; set; }
    }
}
