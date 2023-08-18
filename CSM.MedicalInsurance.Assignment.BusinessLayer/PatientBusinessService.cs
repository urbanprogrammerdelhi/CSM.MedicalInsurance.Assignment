using CSM.MedicalInsurance.Assignment.Models;
using CSM.MedicalInsurance.DataAccessLayer;
using Microsoft.Extensions.Logging;

namespace CSM.MedicalInsurance.Assignment.BusinessLayer
{
    public class PatientBusinessService : IPatientBusinessService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<PatientBusinessService> _logger;
        public PatientBusinessService(IPatientRepository patientRepository,ILogger<PatientBusinessService> logger) { _logger = logger; _patientRepository = patientRepository; }
        public Task<IEnumerable<Patient>> GetAllPatients(PatientSearch patientSearch)
        {
            try
            {
                return _patientRepository.GetAllPatients(patientSearch);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Excepion GetAllPatients");
                throw;
            }
        }

        public Task<PatientInsuranceModel> GetPateientInsuranceDetails(PatientInsuranceSearch patientInsuranceSearch)
        {
            try
            {
                return _patientRepository.GetPateientInsuranceDetails(patientInsuranceSearch);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepion GetPateientInsuranceDetails");
                throw;
            }
        }

        public Task<string> UpdateInsurancePaymentStatus(PatientDetails patientDetails)
        {
            try
            {
                return _patientRepository.UpdateInsurancePaymentStatus(patientDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepion UpdateInsurancePaymentStatus");
                throw;
            }
        }
    }
}