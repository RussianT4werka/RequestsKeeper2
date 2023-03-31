using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestsKeeper2.DB;
using RequestsKeeper2.Models;
using RequestsKeeper2.Tools;
using System.Net.Mail;

namespace RequestsKeeper2.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly user50_4Context user504Context;
        public string Message { get; set; }
        public RegistrationModel(ILogger<IndexModel> logger, user50_4Context user50_4Context)
        {
            _logger = logger;
            user504Context = user50_4Context;
        }

        bool IsValid(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IActionResult OnPost(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Message = "Не все поля заполнены";
                return null;
            }
            var user = user504Context.Visitors.FirstOrDefault(x => x.Email == email);
                if(user == null && IsValid(email))
                {
                    user504Context.Visitors.Add(new Visitor { Email = email, Login = email.Split('@')[0], Password = Hash.HashPass(password) });
                    user504Context.SaveChanges();
                    return RedirectToPage("ListRequestPage", Session.CreateSession(user));
                }
                else
                {
                    Message = "Пользователь с такой электронной почтой уже существует";
                    return null;
                }
            
            
        }
    }
}
