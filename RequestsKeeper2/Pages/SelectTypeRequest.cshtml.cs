using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestsKeeper2.Models;
using RequestsKeeper2.Tools;

namespace RequestsKeeper2.Pages
{
    public class SelectTypeRequestModel : PageModel
    {
        public string UserSession { get; set; }
        public void OnGet(string handler)
        {
            UserSession = handler;
        }
    }
}
