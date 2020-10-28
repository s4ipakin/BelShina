using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.Reports
{
    public class CreatePath
    {
        public static string CreateReportPath(string name, string fileEnding)
        {
            string year = System.DateTime.Now.Year.ToString();
            string month = System.DateTime.Now.Month.ToString();
            string day = System.DateTime.Now.Day.ToString();
            string hour = System.DateTime.Now.Hour.ToString();
            string minute = System.DateTime.Now.Minute.ToString();
            string path = @"D:\Протоколы\" + name + @"\";
            System.IO.Directory.CreateDirectory(path);
            return path + day + "_" + month + "_" + year + "_" + hour + "_" + minute + "_" + name + "_" + fileEnding + ".csv";
        }
    }
}
