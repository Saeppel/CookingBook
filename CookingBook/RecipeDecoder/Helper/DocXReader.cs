using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDecoder.Helper
{
    public class DocXReader
    {
        public DocXReader(string file)
        {
            Path = file;
        }

        public string Path
        {
            get;
            set;
        }

        public void ReadDocument()
        {
            if (!string.IsNullOrWhiteSpace(Path))
            {
                try
                {
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(Path, true))
                    {
                        //Stream stream = File.Open(Path, FileMode.Open);
                        Body body = wordDoc.MainDocumentPart.Document.Body;
                        string content = body.InnerText;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
