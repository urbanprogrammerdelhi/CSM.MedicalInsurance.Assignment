using CSM.MedicalInsurance.Assignment.Models;

namespace CSM.MedicalInsurance.Assignment.Models
{
    public class PatientInsuranceModel
    {
        public Patient PatientDetails { get; set; }
        public PatientInsurance InsuranceCompanyDetails { get; set; }
        public List<PatientBilling> PatientBillings { get; set; }
        public ParentBillingTotal BillingTotal { get; set; }
    }
}
