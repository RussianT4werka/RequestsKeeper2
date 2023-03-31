using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RequestsKeeper2.DB;
using RequestsKeeper2.Models;
using RequestsKeeper2.Tools;

namespace RequestsKeeper2.Pages
{
    public class CreatePersonalRequestModel : PageModel
    {
        [BindProperty]
        public string Message { get; set; }
        public SelectList WorkerList { get; set; }

        [BindProperty]
        public int WorkerId { get; set; }

        [BindProperty]
        public int SubDivisionId { get; set; }

        public SelectList SubDivisionList { get; set; }

        [BindProperty]
        public Request Request { get; set; } = new Request();

        [BindProperty]
        public Visitor Visitor { get; set; }

        private user50_4Context _user50Context;

        public CreatePersonalRequestModel(user50_4Context user50_4Context)
        {
            _user50Context = user50_4Context;
            SubDivisionList = new SelectList(user50_4Context.SubDivisions.ToList(), nameof(SubDivision.Id), nameof(SubDivision.Name));
            SubDivisionId = user50_4Context.SubDivisions.First().Id;
            WorkerList = new SelectList(user50_4Context.Workers.Where( s => s.SubDivisionId == SubDivisionId).ToList(), nameof(Worker.Id), nameof(Worker.FIO));
            Request.StartDate = DateTime.Now.AddDays(1);
            Request.FinishDate = DateTime.Now.AddDays(1);
        }

        public void OnGet(string handler)
        {
            Visitor = Session.GetVisitor(handler);
        }

        public IActionResult OnPost(string handler, string filldata)
        {
            WorkerList = new SelectList(_user50Context.Workers.Where(s => s.SubDivisionId == SubDivisionId).ToList(), nameof(Worker.Id), nameof(Worker.FIO));



            if (!string.IsNullOrEmpty(filldata))
            {
                var file = base.Request.Form.Files.FirstOrDefault(s => s.ContentType == "application/pdf");
                if (string.IsNullOrEmpty(Visitor.Surname)
                || string.IsNullOrEmpty(Visitor.Name)
                || string.IsNullOrEmpty(Visitor.Email)
                || !Visitor.Email.Contains('@')
                || string.IsNullOrEmpty(Visitor.Note)
                || string.IsNullOrEmpty(Visitor.PassportSeries)
                || Visitor.PassportSeries.Length != 4
                || string.IsNullOrEmpty(Visitor.PassportNumber)
                || Visitor.PassportNumber.Length != 6
                || Request.StartDate > DateTime.Now.AddDays(15)
                || Request.FinishDate > Request.StartDate.AddDays(15)
                || Request.StartDate.Month < Request.FinishDate.Month 
                || Request.StartDate.Day <= DateTime.Now.Day && Request.StartDate.Month == DateTime.Now.Month
                || Request.FinishDate.Month < Request.StartDate.Month
                || Request.FinishDate.Day < Request.StartDate.Day && Request.FinishDate.Month == Request.StartDate.Month
                || string.IsNullOrEmpty(Request.TargetVisit)
                || (DateTime.Now - Visitor.DoB).Value.Days / 365 <= 16
                || file == null)
                {
                    Message = "Поля заполнены неверно";
                    return null;
                }
                Visitor.ScanPassport = GetBytesFromFile(file);
                var visitor = Session.GetVisitor(handler);
                Visitor.Id = visitor.Id;
                Visitor.Login = visitor.Login;
                Visitor.Password = visitor.Password;
                _user50Context.Entry(visitor).CurrentValues.SetValues(Visitor);
                Request.WorkerId = WorkerId;
                Request.StatusRequestId = _user50Context.StatusRequests.First().Id;
                Request.TypeRequestId = _user50Context.TypeRequests.First().Id;
                _user50Context.Requests.Add(Request);
                _user50Context.SaveChanges();
                Request = _user50Context.Requests.ToList().Last();
                _user50Context.VisitorRequests.Add(new VisitorRequest{RequestId = Request.Id, VisitorId = Visitor.Id });
                _user50Context.SaveChanges();
            }
            

            return null;
        }

        private byte[] GetBytesFromFile(IFormFile file)
        {
            byte[] data = null;
            using (var fs = file.OpenReadStream())
            {
                data = new byte[fs.Length];
                using (MemoryStream ms = new MemoryStream(data))
                {
                    fs.CopyTo(ms);
                }
            }
            return data;
        }
    }
}
