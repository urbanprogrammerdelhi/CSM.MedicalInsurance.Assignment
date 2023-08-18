using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.MedicalInsurance.Assignment.Utility
{
    public class ApplicationConstants
    {
        public static readonly string HealthInsuranceEmailFormat = "<html>\r\n<span>\r\n    Dear Sir / Madam,\r\n    <br />\r\n    <span>\r\n        I am writing to file a claim on the health insurance policy of @PatientName for the insurance @InsuranceName.\r\n        Please let me know the next steps I need to take to complete the claims process.\r\n        If there is any additional information that you require, please let me know and I will be happy to provide it.\r\n\r\n        Thank you for your attention to this matter.\r\n\r\n        Sincerely,\r\n    </span>\r\n    <br />\r\n    @SenderName\r\n</span>\r\n</html>\r\n";

    }
}
