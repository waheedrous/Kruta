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

            string newFilePath = documentsFolder + documentNumber + ".pdf";

            File.Copy(documentsFolder + "sample10.pdf", newFilePath, true);

            return newFilePath;
        }
    }
}
