using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestsKeeper2.DB;
using RequestsKeeper2.Tools;

namespace RequestsKeeper2.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly user50_4Context user504Context;

        public IndexModel(ILogger<IndexModel> logger, user50_4Context user50_4Context)
        {
            _logger = logger;
            user504Context = user50_4Context;
        }

        public IActionResult OnPost(string login, string password)
        {
            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                Message = "Необходимо заполнить поля для авторизации";
                return null;
            }
            var user = user504Context.Visitors.FirstOrDefault(s => s.Login == login && s.Password == Hash.HashPass(password));
            if(user == null)
            {
                Message = "Неверный логин или пароль";
                return null;
            }
            else
            {
               return RedirectToPage("ListRequestPage", Session.CreateSession(user));
            }
        }
    }
}