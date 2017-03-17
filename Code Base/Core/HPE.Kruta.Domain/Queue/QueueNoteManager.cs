using HPE.Kruta.Common.Enums;
using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System;
using System.Linq;

namespace HPE.Kruta.Domain
{
    /// <summary>
    ///  handles the logic to retrieve and update data in the queue note table
    /// </summary>
    public class QueueNoteManager
    {

        /// <summary>
        /// add a new note for a queue
        /// </summary>
        /// <param name="queueID">the queue to add the note to</param>
        /// <param name="note">the note to add</param>
        /// <param name="employeeID">the employee id added the note</param>
        /// <returns></returns>
        public int Add(int queueID, string note, int employeeID)
        {

            QueueNote queueNote = new QueueNote() { QueueID = queueID, Note = note, CreatedOn = DateTime.Now, CreatedBy = employeeID };

            using (var db = new ModelDBContext())
            {
                db.QueueNotes.Add(queueNote);

                //when adding a note always set the queue status to in progress
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
