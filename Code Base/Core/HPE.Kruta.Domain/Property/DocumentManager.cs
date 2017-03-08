using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HPE.Kruta.Domain
{
    public class DocumentManager
    {
        public string GetDocumentPath( int documentID)
        {
            string documentNumber;

            using (var db = new ModelDBContext())
            {
                documentNumber = db.Documents.Select(d => d.DocumentNumber).First();

            }

            string documentsFolder =  Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "../../Documents/"));

            return @"\Documents\2017-123.pdf";
        }

        public List<DocumentStatus> ListDocumentStatus(bool includeDetails)
        {
            List<DocumentStatus> documentStatuses;
            using (var db = new ModelDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                //db.Configuration.LazyLoadingEnabled = false;
                if (includeDetails)
                {
                    documentStatuses = db.DocumentStatus
                        //.Include(q => q.Document)
                        //.Include(q => q.Document.DocumentStatus)
                        //.Include(q => q.Document.DocumentSubType.DocumentType)
                        //.Include(q => q.Property)
                        //.Include(q => q.QueueStatus)
                        //.Include(q => q.Department)
                        //.Include(q => q.Employee)
                        .ToList();
                }
                else
                {
                    documentStatuses = db.DocumentStatus.ToList();
                }
            }

            return documentStatuses;
        }

    }
}
