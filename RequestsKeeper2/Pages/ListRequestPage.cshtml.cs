using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RequestsKeeper2.DB;
using RequestsKeeper2.Models;
using RequestsKeeper2.Tools;

namespace RequestsKeeper2.Pages
{
    public class ListRequestPageModel : PageModel
    {
        public List<Request> Requests { get; set; } = new List<Request>();
        private readonly user50_4Context user50_4Context;
        public string VisitorSession { get; set; }

        public ListRequestPageModel(user50_4Context user50_4Context)
        {
            this.user50_4Context = user50_4Context;
        }
        public void OnGet(string handler)
        {
            Visitor visitor = Session.GetVisitor(handler);
            VisitorSession = handler;
            Requests = user50_4Context.Requests.Include(s => s.TypeRequest).Include(s => s.Worker).ThenInclude(s => s.SubDivision).Include( s => s.StatusRequest).Where( s => s.VisitorRequests.FirstOrDefault( x => x.VisitorId == visitor.Id) != null).ToList();
        }
    }
}
