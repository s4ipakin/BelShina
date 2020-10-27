using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Data;
using System.Windows;

namespace BelShina_HMI.Reports
{
    public class ReadWriteCSV
    {
        string path;
        public ReadWriteCSV(string path)
        {
            this.path = path;
        }

        public void WriteToCSV(string time, string value, string optValue1 = "", string optValue2 = "", string optValue3 = "")
        {
            using (var w = new StreamWriter(path, true))
            {
                var line = "";
                if (optValue1 == "")
                {
                    line = string.Format("{0};{1}", time, value);
                }
                else if (optValue2 == "")
                {
                    line = string.Format("{0};{1};{2}", time, value, optValue1);
                }
                else if (optValue3 == "")
                {
                    line = string.Format("{0};{1};{2};{3}", time, value, optValue1, optValue2);
                }
                else
                {
                    line = string.Format("{0};{1};{2};{3};{4}", time, value, optValue1, optValue2, optValue3);
                }
                w.WriteLine(line);
                w.Flush();
            }
        }

        public OrderedDictionary ReadFromCSV()
        {
            using (var reader = new StreamReader(path))
            {
                OrderedDictionary fileData = new OrderedDictionary();
                string[] values = { "" };

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    values = line.Split(';');
                    try
                    {
                        fileData.Add(values[0], values[1]);
                    }
                    catch (Exception ex) { }
                }
                return fileData;
            }
        }
    }

    public class CSV_DataTable
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();

            try
            {
                using (StreamReader sr = new StreamReader(strFilePath))
                {
                    string[] headers = sr.ReadLine().Split(';');
                    int orderedNomber = 0;
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(orderedNomber.ToString());
                        orderedNomber++;
                    }
                    while (!sr.EndOfStream)
                    {
                        string[] rows = sr.ReadLine().Split(';');
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                
            }
            catch (Exception ex) 
            {
                
                if (strFilePath == "")
                {
                    MessageBox.Show("Укажите путь к файлу");
                }
            }

            return dt;
        }
    }
}
