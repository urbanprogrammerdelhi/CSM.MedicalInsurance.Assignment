using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM.MedicalInsurance.Assignment.Utility
{
    public class PdfHelper
    {
        public static bool ConvertHtmlToPdf(string htmlString, string filePath)
        {
            try
            {
                HtmlToPdf converter = new HtmlToPdf();
                // set converter options
                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                converter.Options.WebPageWidth = 1024;
                converter.Options.WebPageHeight = 768;
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString);
                // save pdf document
                byte[] pdf = doc.Save();
                // close pdf document
                doc.Close();
                System.IO.File.WriteAllBytes(filePath, pdf);
                if(File.Exists(filePath))
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
}
