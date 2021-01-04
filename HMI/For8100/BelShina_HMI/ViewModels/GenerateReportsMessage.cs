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
        private ushort procNumber;

        public SentDataTab(DataTable dataTable, ushort procNumber)
        {
            this.dataTable = dataTable;
            this.procNumber = procNumber;
        }
        public DataTable DataTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
        public ushort ProcNumber
        {
            get { return procNumber; }
            set { procNumber = value; }
        }
    }

    public class SentDict
    {
        Dictionary<string, ushort> almDict;

        public SentDict(Dictionary<string, ushort> almDict)
        {
            this.almDict = almDict;
        }
        public Dictionary<string, ushort> AlmDict
        {
            get { return almDict; }
            set { almDict = value; }
        }
    }

    public class SentModelName
    {
        string modelName;

        public SentModelName(string modelName)
        {
            this.modelName = modelName;
        }
        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }
    }
}
