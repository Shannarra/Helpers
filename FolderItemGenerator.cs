using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace Helpers
{
    /// <summary>
    /// Gets a path, traverses it and returs a string, containing references to all files.
    /// </summary>
    class FolderItemGenerator
    {

        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        private void WorkWith(string file)
        {
            string containingFolder = Directory.GetParent(file).Name;
            string upperFolder = @"images/italy";


            string cssClass = 
                containingFolder == "01.04 Welcome Fortes" ? "Vicenza" :
                containingFolder == "03.04 saf" ? "Padova" :
                containingFolder == "04.04ITIS Enrico Fermi" ? "Vicenza" :
                containingFolder == "05.04 Frantoio di Valnogaredo  +I_ITT G. Marconi" ? "Padova" :
                containingFolder == "06.04 Venice" ? "Venice" :
                containingFolder == "07.04 Pisa" || containingFolder == "07.04 Florence" ? "Florence" :
                containingFolder == "08.04 Padova" ? "Padova" :
                containingFolder == "11.04 IIS Silvio Ceccato + Castles of Romeo and Juliet" ? "Vicenza" :
                containingFolder == "12.04 Verona" ? "Verona" :
                containingFolder == "16.04 Telwin = La Costa" ? "Vicenza" : "ERROR";

                                 
            string str = $"<li class=\"mix {cssClass}\">\n" +
                         $"      <a class=\"portfolio-popup\" href=\"{upperFolder + @"/" + containingFolder + @"/" + Path.GetFileName(file)}\">\n" +
                         $"          <img src = \"{upperFolder + @"/" + containingFolder + @"/" + Path.GetFileName(file)}\" alt=\"\" />\n" +
                         "          <div class=\"item-overly\">\n" +
                         "              <div class=\"position-center\">\n" +
                         "                  <h4></h4>\n" +
                         "                  <p></p>\n" +
                         "              </div>\n" +
                         "          </div>\n" +
                         "      </a>\n" +
                         "</li>\n";
            builder.Append(str + "\n");
        }


        public FolderItemGenerator()
        {
            //@"C:\Users\Shannarra\Desktop\vicenza\images\italy\"
            string[] files = Directory.GetFiles(@"C:\Users\Shannarra\Desktop\vicenza\images\italy\", "*.*", SearchOption.AllDirectories).Select(x => Path.GetDirectoryName(x) + "/" + Path.GetFileName(x)).ToArray();

            foreach (var item in files)
            {
                WorkWith(item);
            }

            using (StreamWriter w = new StreamWriter(@".\..\..\imgs.xml"))
                w.WriteLine(builder.ToString());

        }
    }
}
