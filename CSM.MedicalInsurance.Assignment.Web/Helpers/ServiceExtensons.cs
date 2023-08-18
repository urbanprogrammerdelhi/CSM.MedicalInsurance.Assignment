using CSM.MedicalInsurance.Assignment.BusinessLayer;
using CSM.MedicalInsurance.Assignment.Utility;
using CSM.MedicalInsurance.DataAccessLayer;

namespace CSM.MedicalInsurance.Assignment.Web
{
    public static class ServiceExtensons
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<MedicalInsuranceContext>();
            services.AddTransient<IPatientBusinessService, PatientBusinessService>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddSingleton<EmailUtility>();

        }
    }
}
