using CSM.MedicalInsurance.Assignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CSM.MedicalInsurance.Assignment.Models
{

    public partial class PatientBilling
    {
        [DisplayName("Slno")]

        public int SlNo { get; set; }
        public int PatientBillingId { get; set; }
        [DisplayName("Date of Billing")]

        public string DateOfBilling { get; set; }
        public int Patientid { get; set; }
        [DisplayName("Perticular")]

        public string Perticular { get; set; }
        [DisplayName("Amount")]

        public decimal Amount { get; set; }
        [DisplayName("Cover %")]

        public string CoverPercentage { get; set; }
        [DisplayName("Convered Amount")]

        public decimal CoveredAmount { get; set; }
        [DisplayName("Self Paid Amount")]

        public decimal SelfPaidAmount { get; set; }
    }
    public class ParentBillingTotal
    {
        public int PatientId { get; set; }
        public int TotalAmount { get; set; }
        public int TotalSelfPaidAmount { get; set; }
        public int TotalCoveredAmount { get; set; }

       
    }
}
