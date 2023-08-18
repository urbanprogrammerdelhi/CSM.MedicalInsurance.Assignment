using CSM.MedicalInsurance.Assignment.BusinessLayer;
using CSM.MedicalInsurance.Assignment.Utility;
using System.Web.Mvc;

namespace CSM.MedicalInsurance.Assignment.Web.Controllers
{
    public class BaseController: Microsoft.AspNetCore.Mvc.Controller
    {
        protected readonly IConfiguration _configuration;
        protected readonly IViewRenderService _viewRender;
        protected readonly EmailUtility _emailUtility;
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger, IConfiguration configuration, IViewRenderService viewRender, EmailUtility emailUtility)
        {
            _logger = logger;
            _configuration = configuration;
            _viewRender = viewRender;
            _emailUtility = emailUtility;
        } 
        
    }
}
