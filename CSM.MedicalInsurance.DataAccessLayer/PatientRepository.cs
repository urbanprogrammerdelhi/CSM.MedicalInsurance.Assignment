using CSM.MedicalInsurance.Assignment.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.MedicalInsurance.DataAccessLayer
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MedicalInsuranceContext _context;
        private readonly ILogger<PatientRepository> _logger;

        public PatientRepository(MedicalInsuranceContext context, ILogger<PatientRepository> logger) { _context = context; _logger = logger; }
        public async Task<IEnumerable<Patient>> GetAllPatients(PatientSearch patientSearch)
        {
            try
            {
                var procedureName = "UDP_Search_PatientsForReimbursement";
                var parameters = new DynamicParameters();
                parameters.Add("@ReimbursementPeriod", patientSearch.DischargePeriodsInDays, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PatientId", patientSearch.PatientId, DbType.Int32, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var company = await connection.QueryAsync<Patient>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return company;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepion GetAllPatients");
                throw;
            }
        }

        public async Task<PatientInsuranceModel> GetPateientInsuranceDetails(PatientInsuranceSearch patientInsuranceSearch)
        {
            try
            {

                var procedureName = "UDP_Search_PatientsForReimbursementClaimDetails";
                var parameters = new DynamicParameters();
                parameters.Add("@PatientId", patientInsuranceSearch.PatientId, DbType.Int32, ParameterDirection.Input);
                PatientInsuranceModel result = new PatientInsuranceModel();
                using (var connection = _context.CreateConnection())
                {
                    //var company = await connection.QueryMultiple(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    //return company;

                    IEnumerable<dynamic> results = connection.Query(sql: procedureName, param: parameters, commandType: CommandType.StoredProcedure);
                    var reader = connection.QueryMultiple(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    result.PatientDetails = reader.Read<Patient>().FirstOrDefault();
                    result.InsuranceCompanyDetails = reader.Read<PatientInsurance>().FirstOrDefault();
                    result.PatientBillings = reader.Read<PatientBilling>().ToList();
                    result.BillingTotal = reader.Read<ParentBillingTotal>().FirstOrDefault();
                    return result;


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepion GetPateientInsuranceDetails");
                throw;
            }
        }

        public async Task<string> UpdateInsurancePaymentStatus(PatientDetails patientDetails)
        {
            try
            {
                var procedureName = "UDP_Put_PatientInsurancePaymentStatus";
                var parameters = new DynamicParameters();
                parameters.Add("@InsuranceStatus", patientDetails.InsurancePaymentStatus, DbType.String, ParameterDirection.Input);
                parameters.Add("@PatientId", patientDetails.PatientId, DbType.Int32, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    return await connection.QuerySingleAsync<string>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepion UpdateInsurancePaymentStatus");
                throw;
            }
        }
    }
}
