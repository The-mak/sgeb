using Mvc.Mailer;

namespace SGEB.Mailers
{ 
    public interface IUserMailer
    {
		MvcMailMessage Welcome();
	}
}