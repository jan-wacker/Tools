using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Diagnostics;

namespace Startup
{
    public class EMailVersender
    {
        /// <summary>
        /// Versendet eine Email.
        /// </summary>
        public static void SendeEmail(string betreff, string nachricht)
        {
            Configuration oConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var mailSettings = oConfig.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            if (mailSettings != null)
            {
                int port = mailSettings.Smtp.Network.Port;
                string host = mailSettings.Smtp.Network.Host;
                string passwort = mailSettings.Smtp.Network.Password;
                string nutzername = mailSettings.Smtp.Network.UserName;

                string emaiAn = ConfigurationManager.AppSettings.Get("EMail-An");
                MailMessage mailMessage = new MailMessage(nutzername, emaiAn, betreff, nachricht);

                SmtpClient smtpClient = new SmtpClient()
                {
                    Host = host,
                    Port = port,
                    Credentials = new NetworkCredential(nutzername, passwort),
                    EnableSsl = true
                };

                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    using (EventLog eventLog = new EventLog("Application"))
                    {
                        eventLog.Source = "Application";
                        eventLog.WriteEntry(ex.Message, EventLogEntryType.Error, 101, 1);
                    }

                }
            }
        }
    }
}
