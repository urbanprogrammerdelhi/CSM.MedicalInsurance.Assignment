using CSM.MedicalInsurance.Assignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSM.MedicalInsurance.Assignment.Models
{

    public class Patient
    {
        public int PatientId { get; set; }
        [DisplayName("Patient Name")]
        public string PatientName { get; set; }
        [DisplayName("Address")]

        public string PatientAddress { get; set; }
        [DisplayName("Phone number")]

        public string PatientPhoneNumber { get; set; }
        [DisplayName("Email")]

        public string PatientEmail { get; set; }
    }
}
