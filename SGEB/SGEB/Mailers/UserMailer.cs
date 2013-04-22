using Mvc.Mailer;
using System.IO;
using SGEB.Model;
using System.Collections;
using System.Collections.Generic;

namespace SGEB.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage Welcome()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "Welcome";
				x.ViewName = "Welcome";
				x.To.Add("some-email@example.com");
			});
		}

        public virtual MvcMailMessage Welcome(string email, MemoryStream stream, IList<string> files)
        {
            return Populate(x =>
            {
                x.Subject = "[BIII TRANSPORTES] Ficha";
                x.ViewName = "Welcome";
                x.To.Add(email);
                x.Attachments.Add(new System.Net.Mail.Attachment(stream, "ficha.pdf", "application/pdf"));

                foreach (string file in files)
                {
                    if (File.Exists(file))
                        x.Attachments.Add(new System.Net.Mail.Attachment(file, new System.Net.Mime.ContentType("image/jpeg")));
                }
            });
        }
 	}
}