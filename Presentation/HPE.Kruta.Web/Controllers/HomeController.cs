using HPE.Kruta.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {

            //QueueNoteManager m1 = new QueueNoteManager();

            //m1.Add(13, "test from the app");


            //QueueManager m = new QueueManager();

            //m.AssignEmployeeBulk(new List<int> {12,13,14,15,16 } , 3);

            //string a = q.Property.ParcelNumber;
            //string a1 = q.Document.DocumentSubType.DocumentType.Description;


            return View();
        }

        public void LoadAssignToList()
        {
            //TODO
        }
    }
}