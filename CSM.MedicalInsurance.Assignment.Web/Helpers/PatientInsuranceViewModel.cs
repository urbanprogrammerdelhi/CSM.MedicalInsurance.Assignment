using CSM.MedicalInsurance.Assignment.Models;
using CSM.MedicalInsurance.Assignment.Models.Domain_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using CSM.MedicalInsurance.Assignment.Utility;
namespace CSM.MedicalInsurance.Assignment.Web
{
    public class PatientInsuranceViewModel
    {
        [DisplayName("Patient:")]
        public List<SelectListItem> PatientList { get; set; }
        public string SelectedPatient { get; set; }

        public PatientInsuranceModel PatientInsuranceDetails { get; set; }
        public PatientBilling DefaultBillingDetails { get; set; }
        public Hospital HospitalDetails { get; set; }

        public static PatientInsuranceViewModel GenerateDefaultViewModel(IConfiguration configuration)
        {
            PatientInsuranceViewModel vm = new PatientInsuranceViewModel
            {
                DefaultBillingDetails=new PatientBilling(),
                PatientInsuranceDetails=null,
                PatientList=new List<SelectListItem>(),
                SelectedPatient=string.Empty
            };
            if (configuration != null)
            {
                vm.HospitalDetails = new Assignment.Models.Domain_Models.Hospital
                {
                    Area = configuration["HospitalAddress:Area"].ParseToText(),
                    Name = configuration["HospitalAddress:Name"].ParseToText(),
                    City = configuration["HospitalAddress:City"].ParseToText(),
                    Email = configuration["HospitalAddress:Email"].ParseToText(),
                    Phone = configuration["HospitalAddress:Phone"].ParseToText(),
                    PinCode =configuration["HospitalAddress:PinCode"].ParseToText(),
                    Street = configuration["HospitalAddress:Street"].ParseToText()
                };
                using (FileStream stream = new FileStream(Directory.GetCurrentDirectory() + @$"\Logos\{configuration["HospitalAddress:Logo"].ParseToText()}", FileMode.Open))
                {
                    var bytes = stream.GetBytes();
                    var base64String = Convert.ToBase64String(bytes);
                    vm.HospitalDetails.Logo = "data:image/jpg;base64," + base64String;

                }
            }
            
            return vm;
        }

    }
}
