using HPE.Kruta.DataAccess;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.IO;


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
    }
}
