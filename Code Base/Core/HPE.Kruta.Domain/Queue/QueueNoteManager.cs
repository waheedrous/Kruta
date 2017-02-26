using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Domain
{
    public class QueueNoteManager
    {

        public int Add(int queueID, string note, int employeeID)
        {
            //todo: get employee id from session

            QueueNote queueNote = new QueueNote() { QueueID = queueID, Note = note, CreatedOn = DateTime.Now, CreatedBy = employeeID };

            using (var db = new ModelDBContext())
            {
                db.QueueNotes.Add(queueNote);
                db.SaveChanges();
            }

            return queueNote.QueueNoteID;
        }

    }
}
