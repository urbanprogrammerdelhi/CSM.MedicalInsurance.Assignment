﻿using CSM.MedicalInsurance.Assignment.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CSM.MedicalInsurance.Assignment.Utility
{
    public class EmailUtility
    {
        private readonly ILogger<EmailUtility> _logger;
        public EmailUtility(ILogger<EmailUtility> logger)
        {
            _logger = logger;
        }
        public bool SendMail(MessageBody mailMessage)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                     | SecurityProtocolType.Tls11
                                     | SecurityProtocolType.Tls12;
            
                MailMessage mm = new MailMessage(mailMessage.SmTpDetails.Sender, mailMessage.Receiver);
                mm.Subject = mailMessage.EmailSubject;
                mm.Body = mailMessage.EmailBody;
                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(new FileStream(mailMessage.Attachment, FileMode.OpenOrCreate), mailMessage.AttachmentName));
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = mailMessage.SmTpDetails.SmtpHost;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.SmTpDetails.Credentials[0];
                NetworkCred.Password = mailMessage.SmTpDetails.Credentials[1];
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = mailMessage.SmTpDetails.SmtpPort.ParseToInteger();
                smtp.Send(mm);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while sending email");
            }
            return false;
        }
    }
}
