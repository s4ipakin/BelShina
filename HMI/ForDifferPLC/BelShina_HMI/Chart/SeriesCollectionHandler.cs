﻿using LiveCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.Chart
{
    public class SeriesCollectionHandler
    {
        public string[] SetValues(IChartValues collection, DataTable dataTable, int dataXcolumn, int dataYcolumn)
        {
            string[] labels = new string[dataTable.Rows.Count];
            Queue<string> queue = new Queue<string>();
            collection.Clear();

            object[] values = new object[dataTable.Rows.Count];

            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                queue.Enqueue(dataTable.Rows[j][dataXcolumn].ToString());
                double st = Convert.ToDouble(dataTable.Rows[j][dataYcolumn]);
                collection.Add(st);
            }
            labels = queue.ToArray();
            return labels;
        }

        
    }
}
