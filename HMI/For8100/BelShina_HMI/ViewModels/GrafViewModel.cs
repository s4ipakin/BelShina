using BelShina_HMI.Chart;
using BelShina_HMI.OPC;
using BelShina_HMI.Reports;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Workstation.ServiceModel.Ua;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class GrafViewModel : SubscriptionBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels
        {
            get { return this._labels; }
            set { this.SetProperty(ref this._labels, value); }
        }
        
        protected string[] _labels;
        protected GrafSeries[] arSeries;
        public Func<double, string> YFormatter { get; set; }
        protected string firstValueName;
        protected string secondValueName;

        protected SeriesCollectionHandler seriesCollectionHandler = new SeriesCollectionHandler();
        protected DataTable dataTable = new DataTable();

        /// ///////////////////////
        protected GrafSet grafSet;
        protected string cSvPath;
        protected OPC_UA_Client OPC_UA;
        protected string grafValueX;
        protected string grafValueY;
       
        public GrafViewModel(GrafSet grafSet, string cSvPath)
        {
            this.grafSet = grafSet;
            this.cSvPath = cSvPath;
            arSeries = this.grafSet.GetSettings();
            SeriesCollection = new SeriesCollection();
            Labels = new[] { System.DateTime.Now.ToString() };
            YFormatter = value => value.ToString() + this.grafSet.unit;
            dataTable.Columns.Add("FirstValue");
            dataTable.Columns.Add("SecondValue");
            YaxesName = grafSet.yAxesName;
            YaxesMaxValue = grafSet.maxValue;
            XaxesName = grafSet.xAxesName;
            for (int i = 0; i < arSeries.Length; i++)
            {
                SeriesCollection.Add(arSeries[i].LineSeries);
            }
            ListOfItemsOPC listOfItemsOPC = new ListOfItemsOPC();
            OPC_UA = new OPC_UA_Client("192.168.1.17", 2000d, listOfItemsOPC.GetOPCitems());
            //OPC_UA.ItemsChanged += OPC_UA_ItemsChanged;
            Messenger.Default.Register<GenerateReportsMessage>(this, GenerateReports);
            _ = Task();
        }

        protected string yaxesName;
        public string YaxesName
        {
            get { return yaxesName; }
            set { yaxesName = value; }
        }

        protected double yaxesMaxValue;
        public double YaxesMaxValue
        {
            get { return yaxesMaxValue; }
            set { yaxesMaxValue = value; }
        }

        protected string xaxesName;
        public string XaxesName
        {
            get { return xaxesName; }
            set { xaxesName = value; }
        }

        

        protected async Task Task()
        {
            await OPC_UA.SetChanel();
        }
        protected Dictionary<string, string> itemDict = new Dictionary<string, string>();


        /// /////////////////////// ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.wFS_ActualPos

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.rFS_GetForce_check")]
        public virtual float ActualPosition
        {
            get { return this.actualPosition; }
            set { this.SetProperty(ref this.actualPosition, value); }
        }

        private float actualPosition;
        
        /// ///////////////////////////////////////////////
        

        //[MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.rFS_GetForce")]
        public virtual float GetForse
        {
            get 
            {
               
                return this.getForse;
                
            }
            set { this.SetProperty(ref this.getForse, value); }
        }

        private float getForse;
        /// ///////////////////////////////////////////////

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.wFS_State")]
        public virtual ushort FS_State
        {
            get { return this.fS_State; }
            set { this.SetProperty(ref this.fS_State, value); }
        }

        private ushort fS_State;

        

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.xProcFinished")]
        public virtual bool ProcFinished
        {
            get {return this.procFinished;}
            set { this.SetProperty(ref this.procFinished, value); }
        }

        protected bool procFinished;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.wGC_Distance_1")]
        public virtual ushort ProcType_1
        {
            get { return this.wProcType_1; }
            set { this.SetProperty(ref this.wProcType_1, value); }
        }

        private ushort wProcType_1;

        protected async void Read(string name1, string name2)
        {
            if (OPC_UA != null)
            {
                try
                {
                    //MessageBox.Show("get");
                    itemDict = await OPC_UA.ReadOPCAsync();
                   
                    grafValueX = itemDict[name1];
                    grafValueY = itemDict[name2];
                    

                }
                catch (Exception ex) { }
            }
        }

        protected void SaveToCSV(bool start, string name, string column1, string column2)
        {
            //MessageBox.Show(start.ToString() + " ; " + dataTable.Rows.Count.ToString());
            if (start && dataTable.Rows.Count > 2)
            {           
                string year = System.DateTime.Now.Year.ToString();
                string month = System.DateTime.Now.Month.ToString();
                string day = System.DateTime.Now.Day.ToString();
                string hour = System.DateTime.Now.Hour.ToString();
                string minute = System.DateTime.Now.Minute.ToString();
                ReadWriteCSV readWriteCSV = new ReadWriteCSV(@"D:\Протоколы\" + hour + "_" + minute + "_" + day + "_" + month + "_" + year + "_" + name + ".csv");
                readWriteCSV.WriteToCSV(column1, column2);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    readWriteCSV.WriteToCSV(dataTable.Rows[i][0].ToString(), dataTable.Rows[i][1].ToString());
                }
            }
        }

        protected bool run = false;
        protected void GetGrafPoints()
        {
            if ((grafValueY != null) && (grafValueY != ""))
            {
                if (!run)
                {
                    dataTable.Clear();
                    run = true;
                }
                DataRow dr = dataTable.NewRow();
                dr[0] = grafValueX;
                dr[1] = grafValueY;
                dataTable.Rows.Add(dr);
                Labels = seriesCollectionHandler.SetValues(SeriesCollection[0].Values, dataTable, 0, 1);
            }
            else 
            {
                run = false;
            }
        }

        public virtual void GenerateReports(GenerateReportsMessage generate)
        {

        }
    }
}
