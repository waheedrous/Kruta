using HPE.Kruta.Domain;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.IO;
using HPE.Kruta.Common.Enum;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {

            //DocumentManager dm = new DocumentManager();

            //dm.GetDocumentPath(2);

            //QueueNoteManager m1 = new QueueNoteManager();

            //m1.Add(13, "test from the app");


            //QueueManager m = new QueueManager();

            //m.AssignEmployeeBulk(new List<int> {12,13,14,15,16 } , 3);

            //string a = q.Property.ParcelNumber;
            //string a1 = q.Document.DocumentSubType.DocumentType.Description;

            LoadAssignToList();
            return View();
        }

        public void LoadAssignToList()
        {
            var emps = _userManager.List().OrderBy(o => o.EmployeeName);
            ViewBag.RoutingControlStaffList = new SelectList(emps, "EmployeeID", "EmployeeName");


        }
    }
}