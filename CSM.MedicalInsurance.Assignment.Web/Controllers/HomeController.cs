using CSM.MedicalInsurance.Assignment.BusinessLayer;
using CSM.MedicalInsurance.Assignment.Web.Models;
using CSM.MedicalInsurance.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSM.MedicalInsurance.Assignment.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientBusinessService _patientBusinessService;

        public HomeController(ILogger<HomeController> logger,IPatientBusinessService patientBusinessService)
        {
            _logger = logger;
            _patientBusinessService = patientBusinessService;
        }

        public IActionResult Index()
        {
            var test = _patientBusinessService.GetPateientInsuranceDetails(new Assignment.Models.PatientInsuranceSearch {PatientId=4 });
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}