using HPE.Kruta.Common.Enum;
using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System;
using System.Linq;

namespace HPE.Kruta.Domain
{
    public class QueueNoteManager
    {

        public int Add(int queueID, string note, int employeeID)
        {

            QueueNote queueNote = new QueueNote() { QueueID = queueID, Note = note, CreatedOn = DateTime.Now, CreatedBy = employeeID };

            using (var db = new ModelDBContext())
            {
                db.QueueNotes.Add(queueNote);

                var inProgressQueueStatus = db.QueueStatus.FirstOrDefault(q => q.QueueStatusID == (int)QueueStatusEnum.InProgress);
                if (inProgressQueueStatus != null)
                {
                    var queue = db.Queues.Where(q => q.QueueID == queueID).First();
                    queue.QueueStatusID = inProgressQueueStatus.QueueStatusID;

                }
                db.SaveChanges();
            }

            return queueNote.QueueNoteID;
        }

    }
}
