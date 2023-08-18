using CSM.MedicalInsurance.Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.MedicalInsurance.DataAccessLayer
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients(PatientSearch patientSearch);
        Task<PatientInsuranceModel> GetPateientInsuranceDetails(PatientInsuranceSearch patientInsuranceSearch);
        Task<string> UpdateInsurancePaymentStatus(PatientDetails patientDetails);
    }
}
