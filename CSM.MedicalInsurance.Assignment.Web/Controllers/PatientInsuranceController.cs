using CSM.MedicalInsurance.Assignment.BusinessLayer;
using CSM.MedicalInsurance.Assignment.Utility;
using CSM.MedicalInsurance.Assignment.Web.Models;
using CSM.MedicalInsurance.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net.Mail;
using System.Security.Cryptography.Xml;
using System.Text;
using Microsoft.AspNetCore.Html;
using SelectPdf;
using CSM.MedicalInsurance.Assignment.Models;
using Microsoft.AspNetCore.Components.RenderTree;

namespace CSM.MedicalInsurance.Assignment.Web.Controllers
{
    public class PatientInsuranceController : BaseInsuranceController
    {
       
        public PatientInsuranceController(ILogger<PatientInsuranceController> logger, IPatientBusinessService patientBusinessService, IConfiguration configuration, IViewRenderService viewRender,EmailUtility emailUtility):base(logger,configuration,viewRender,emailUtility,patientBusinessService)
        {
           
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if(TempData.TryFetch<MessageInfo>("MessageInfo",out MessageInfo messageInfo))
                {
                    ViewBag.MessageInfo = messageInfo;
                }
                PatientInsuranceViewModel vm = await LoadPatientInsuranceViewModel();
                TempData["PatientId"] = null;
                return View(vm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }
        [HttpPost]
        public IActionResult SubmitRequest(string Submit)
        {
            try
            {
                if(!Request.Form.Keys.Contains("SelectedPatient"))
                {
                    TempData.Put("MessageInfo",new MessageInfo { Message = "Please select a parent to continue", HasIssue = true });
                    return RedirectToAction("Index");
                }
                TempData["PatientId"] = Request.Form["SelectedPatient"].ParseToText();
                if(string.IsNullOrEmpty(Submit))
                    return RedirectToAction("Index");
                switch (Submit)
                {
                    case "Show Insurance Details":
                        return RedirectToAction("Index");
                        break;
                    case "Create PDF and Send that PDF to Insurance Company":
                       return RedirectToAction("NotifyCompany");

                        break;
                     
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
                return RedirectToAction("Index", "Error");

            }
        }

        public async Task<IActionResult> NotifyCompany()
        {
            try
            {
                PatientInsuranceViewModel vm = await LoadPatientInsuranceViewModel();
                var html = await _viewRender.RenderToStringAsync("_PatientInsuranceDetails", vm);
                string reportPath = Directory.GetCurrentDirectory() + @$"\InsuranceDocuments\{vm.PatientInsuranceDetails.PatientDetails.PatientId}_{vm.PatientInsuranceDetails.PatientDetails.PatientName}_{DateTime.Now.ToString("ddMMMyyyyhhmmss")}.pdf";
                bool reportGenerated = PdfHelper.ConvertHtmlToPdf(html, reportPath);
                if(!reportGenerated)
                {
                    TempData.Put("MessageInfo",$"Unable to generate the Claim details for the patient {vm.PatientInsuranceDetails.PatientDetails.PatientName}");
                    return RedirectToAction("Index");
                }
                MessageBody message = PrepareMailMessage(reportPath, $"InsuranceClaim_{vm.PatientInsuranceDetails.PatientDetails.PatientName}.pdf", vm);
                if (_emailUtility.SendMail(message))
                {
                    _patientBusinessService.UpdateInsurancePaymentStatus(new PatientDetails {InsurancePaymentStatus= "Invoice Send",PatientId=vm.PatientInsuranceDetails.PatientDetails.PatientId });
                    TempData.Put("MessageInfo", new MessageInfo { Message = $"Successfully sent the Claim details of {vm.PatientInsuranceDetails.PatientDetails.PatientName} to the respective insurance company {vm.PatientInsuranceDetails.InsuranceCompanyDetails.InsuranceCompanyName}", HasIssue = false });
                }
                else
                {
                    TempData.Put("MessageInfo", new MessageInfo { Message = $"Unable to send the Claim details for the patient {vm.PatientInsuranceDetails.PatientDetails.PatientName} to the insurance company {vm.PatientInsuranceDetails.InsuranceCompanyDetails.InsuranceCompanyName}", HasIssue = true });

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
                TempData.Put("MessageInfo",new MessageInfo { Message = $"There was an problem generating the Claim details for the selected patient", HasIssue = true });


            }
            return RedirectToAction("Index");

        }
     
    }
}

