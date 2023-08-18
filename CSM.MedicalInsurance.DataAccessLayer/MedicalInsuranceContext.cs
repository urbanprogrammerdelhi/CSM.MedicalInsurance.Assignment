using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CSM.MedicalInsurance.DataAccessLayer
{
    public class MedicalInsuranceContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public MedicalInsuranceContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}