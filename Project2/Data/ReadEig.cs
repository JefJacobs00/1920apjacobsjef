using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReadEig
    {

        public ReadEig()
        {
        }

        public Dictionary<string,double> ReadEigenSchappen()
        {
            Dictionary<string, double> eigenschappen = new Dictionary<string, double>();

            string fileRead;
            try
            {
                using (StreamReader r = new StreamReader(@"../../../Data/Resources/eigenschappen.csv"))
                {
                    fileRead = r.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }

            while (fileRead.IndexOf("\r\n") > -1)
            {
                fileRead = fileRead.Replace("\r\n", "");
            }

            do
            {
                string province = fileRead.Substring(0,fileRead.IndexOf(';'));
                fileRead = fileRead.Substring(fileRead.IndexOf(';')+1);
                string eig = fileRead.Substring(0,fileRead.IndexOf(';'));
                fileRead = fileRead.Substring(fileRead.IndexOf(';')+1);
                eigenschappen.Add(province, double.Parse(eig));
            } while (fileRead.IndexOf(';') != -1);

            return eigenschappen;
        }
    }
}
