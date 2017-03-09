using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HPE.Kruta.Domain
{
    /// <summary>
    /// handles the logic to retrieve and update data in the document table
    /// </summary>
    public class DocumentManager
    {
        /// <summary>
        /// return the document for the specified id
        /// </summary>
        /// <param name="documentID">documentid for the document to return</param>
        /// <returns></returns>
        public string GetDocumentPath(int documentID)
        {
            string documentNumber;

            using (var db = new ModelDBContext())
            {
                documentNumber = db.Documents.Select(d => d.DocumentNumber).First();

            }

            string documentsFolder = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "../../Documents/"));

            return @"\Documents\2017-123.pdf";
        }

        /// <summary>
        /// returns a list of all document statuses
        /// </summary>
        /// <returns></returns>
        public List<DocumentStatus> ListDocumentStatus()
        {
            List<DocumentStatus> documentStatuses;
            using (var db = new ModelDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                documentStatuses = db.DocumentStatus.ToList();
            }
        

            return documentStatuses;
        }

}
}
