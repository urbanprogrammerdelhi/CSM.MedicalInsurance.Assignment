using CSM.MedicalInsurance.Assignment.BusinessLayer;
using CSM.MedicalInsurance.Assignment.Models;
using CSM.MedicalInsurance.Assignment.Utility;

namespace CSM.MedicalInsurance.Assignment.Web.Controllers
{
    public class BaseInsuranceController:BaseController
    {
        protected IPatientBusinessService _patientBusinessService;
        public BaseInsuranceController(ILogger<BaseController> logger,
            IConfiguration configuration, IViewRenderService viewRender,
            EmailUtility emailUtility, IPatientBusinessService patientBusinessService) :base(logger, configuration, viewRender, emailUtility)
        {
            _patientBusinessService=patientBusinessService;
        }
        protected async Task<PatientInsuranceViewModel> LoadPatientInsuranceViewModel()
        {
            try
            {
                PatientInsuranceViewModel vm = PatientInsuranceViewModel.GenerateDefaultViewModel(_configuration);
                var patientSearch = new Assignment.Models.PatientSearch { PatientId = null, DischargePeriodsInDays = _configuration["InsurancePeriod"].ParseToInteger() };
                var patientList = await _patientBusinessService.GetAllPatients(patientSearch);
                vm.PatientList = patientList.ToList().ToSelectList("PatientId", "PatientName");
                if (TempData.TryGetValue("PatientId", out var patientId) && !string.IsNullOrEmpty(patientId.ParseToText()))
                {
                    vm.SelectedPatient = patientId.ParseToText();
                    vm.PatientInsuranceDetails = await _patientBusinessService.GetPateientInsuranceDetails(new Assignment.Models.PatientInsuranceSearch { PatientId = vm.SelectedPatient.ParseToInteger() });
                }
                else
                {
                    vm.PatientInsuranceDetails = null;
                }
                return vm;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
                throw;
            }
        }
        protected MessageBody PrepareMailMessage(string attachment, string attachmentName, PatientInsuranceViewModel vm)
        {
            try
            {
                MessageBody message = new MessageBody { Attachment = attachment, AttachmentName = attachmentName, Receiver = vm.PatientInsuranceDetails.InsuranceCompanyDetails.InsuranceContactEmail };
                message.EmailSubject = _configuration["EmailUtility:Subject"].ParseToText();
                string userName = _configuration["EmailUtility:UserName"].ParseToText();
                string password = _configuration["EmailUtility:Password"].ParseToText();
                message.SmTpDetails = new SmTpDetails
                {
                    Credentials = new string[] { userName, password },
                    Sender = _configuration["EmailUtility:Sender"].ParseToText(),
                    SmtpHost = _configuration["EmailUtility:SmtpServer"].ParseToText(),
                    SmtpPort = _configuration["EmailUtility:Port"].ParseToText(),
                    SenderName = _configuration["EmailUtility:SenderName"].ParseToText()

                };
                string emailBody = ApplicationConstants.HealthInsuranceEmailFormat.Replace("@PatientName", vm.PatientInsuranceDetails.PatientDetails.PatientName);
                emailBody = emailBody.Replace("@InsuranceName", vm.PatientInsuranceDetails.InsuranceCompanyDetails.InsuranceCompanyName);
                emailBody = emailBody.Replace("@SenderName", message.SmTpDetails.SenderName);
                message.EmailBody = emailBody;
                return message;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
                throw;
            }

        }
    }
}
