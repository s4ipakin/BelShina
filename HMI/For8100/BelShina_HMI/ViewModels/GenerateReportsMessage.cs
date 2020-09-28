using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.ViewModels
{
    public class GenerateReportsMessage
    {
    }

    public class SentDataTab
    {
        private DataTable dataTable;

        public SentDataTab(DataTable dataTable)
        {
            this.dataTable = dataTable;
        }
        public DataTable DataTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
    }
}
