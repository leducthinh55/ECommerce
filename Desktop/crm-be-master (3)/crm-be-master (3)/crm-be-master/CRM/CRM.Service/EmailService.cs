using CRM.Service.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Service
{
    public class FileAttachmentModel
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
    }
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message, List<string> cc, List<FileAttachmentModel> fileAttachments);
        Task SendListEmail(string[] emails, string subject, string message, List<string> cc, List<FileAttachmentModel> fileAttachments);
    }
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string email, string subject, string message, List<string> cc, List<FileAttachmentModel> fileAttachments)
        {
            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Host = "210.2.92.5";
            client.Port = 25;
            client.Credentials = new System.Net.NetworkCredential("crm.test", "Qtsc#2020#");
            client.EnableSsl = false;
            objeto_mail.Subject = subject;
            objeto_mail.From = new MailAddress("crm.test@qtsc.com.vn", "Quang Trung Sofware City");
            objeto_mail.To.Add(new MailAddress(email));
            objeto_mail.Body = message;
            objeto_mail.IsBodyHtml = true;
            if (cc != null)
            {
                foreach (var item in cc)
                {
                    objeto_mail.CC.Add(new MailAddress(item));
                }
            }
            if (fileAttachments != null)
            {
                foreach (var item in fileAttachments)
                {
                    item.FileStream.Position = 0;
                    ContentType ct = new System.Net.Mime.ContentType(FileUtils.GetContentType(item.FileName));
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(item.FileStream, ct);
                    attach.ContentDisposition.FileName = item.FileName;
                    objeto_mail.Attachments.Add(attach);
                }
                await client.SendMailAsync(objeto_mail);

                foreach (var att in objeto_mail.Attachments)
                {
                    att.Dispose();
                }
            }
            else
            {
                try
                {
                    await client.SendMailAsync(objeto_mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


        }

        public async Task SendListEmail(string[] emails, string subject, string message, List<string> cc, List<FileAttachmentModel> fileAttachments)
        {
            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Host = "210.2.92.5";
            client.Port = 25;
            client.Credentials = new System.Net.NetworkCredential("crm.test", "Qtsc#2020#");
            client.EnableSsl = false;
            objeto_mail.Subject = subject;
            objeto_mail.From = new MailAddress("crm.test@qtsc.com.vn", "Quang Trung Sofware City");
            foreach(string email in emails)
            {
                objeto_mail.To.Add(new MailAddress(email.Trim()));
            }
            objeto_mail.Body = message;
            objeto_mail.IsBodyHtml = true;
            if (cc != null)
            {
                foreach (var item in cc)
                {
                    objeto_mail.CC.Add(new MailAddress(item));
                }
            }
            if (fileAttachments != null)
            {
                foreach (var item in fileAttachments)
                {
                    item.FileStream.Position = 0;
                    ContentType ct = new System.Net.Mime.ContentType(FileUtils.GetContentType(item.FileName));
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(item.FileStream, ct);
                    attach.ContentDisposition.FileName = item.FileName;
                    objeto_mail.Attachments.Add(attach);
                }
                await client.SendMailAsync(objeto_mail);

                foreach (var att in objeto_mail.Attachments)
                {
                    att.Dispose();
                }
            }
            else
            {
                try
                {
                    await client.SendMailAsync(objeto_mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


        }
    }
}
