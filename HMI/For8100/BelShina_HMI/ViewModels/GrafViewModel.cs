using BelShina_HMI.Chart;
using BelShina_HMI.Maths;
using BelShina_HMI.OPC;
using BelShina_HMI.Reports;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Configurations;
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
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 2)]
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
        protected float previousPosition;
        protected ushort procNomber;

        public ICommand ButtonChooseFile_1 { get; set; }
        public ICommand ButtonBuidGraf_1 { get; set; }
        public ICommand ButtonChooseFile_2 { get; set; }
        public ICommand ButtonBuidGraf_2 { get; set; }
        public ICommand ButtonBuildBack { get; set; }

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
            SeriesName = grafSet.seriesName;
            for (int i = 0; i < arSeries.Length; i++)
            {
                SeriesCollection.Add(arSeries[i].LineSeries);
            }

            ButtonChooseFile_1 = new RelayCommand(o => OpenFile_1("ReportsButton"));
            ButtonBuidGraf_1 = new RelayCommand(o => ShowFromCSV_1("ReportsButton"));
            ButtonChooseFile_2 = new RelayCommand(o => OpenFile_2("ReportsButton"));
            ButtonBuidGraf_2 = new RelayCommand(o => ShowFromCSV_2("ReportsButton"));
            ButtonBuildBack = new RelayCommand(o => BuildBack("ReportsButton"));

            var mapper = Mappers.Xy<MeasureModel>()
               .X(model => model.ValueX)   
               .Y(model => model.ValueY);
            Charting.For<MeasureModel>(mapper);
            ChartValues = new ChartValues<MeasureModel>();
            ListOfItemsOPC listOfItemsOPC = new ListOfItemsOPC();
            OPC_UA = new OPC_UA_Client("192.168.1.17", 2000d, listOfItemsOPC.GetOPCitems());
            //OPC_UA.ItemsChanged += OPC_UA_ItemsChanged;
            Messenger.Default.Register<GenerateReportsMessage>(this, GenerateReports);
            Messenger.Default.Register<SentModelName>(this, SetFileNameEnding);
            _ = Task();
        }

        protected string fileEnding = "";

        protected void SetFileNameEnding(SentModelName obj)
        {
            fileEnding = obj.ModelName;
        }

        public ChartValues<MeasureModel> ChartValues { get; set; }

        protected string seriesName;
        public string SeriesName
        {
            get { return seriesName; }
            set { seriesName = value; }
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

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.xFS_Start")]
        public virtual bool Start
        {
            get
            {
                
                return this.start;
            }
            set { this.SetProperty(ref this.start, value); }
        }
        private bool start;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.diDistanceForce")]
        public virtual int DistanceForce
        {
            get { return this.distanceForce; }
            set { this.SetProperty(ref this.distanceForce, value); }
        }
        private int distanceForce;

        //private bool xStarted = false;

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
                ReadWriteCSV readWriteCSV = new ReadWriteCSV(CreatePath.CreateReportPath(name, fileEnding));
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
                    ChartValues.Clear();
                    grafValueX = "0";
                    grafValueY = "0";

                    run = true;
                }
                DataRow dr = dataTable.NewRow();
                dr[0] = grafValueX;
                dr[1] = grafValueY;
                dataTable.Rows.Add(dr);

                ChartValues.Add(new MeasureModel
                {
                    ValueX = Convert.ToDouble(grafValueX),
                    ValueY = Convert.ToDouble(grafValueY)
                });
                Labels = seriesCollectionHandler.SetValues(SeriesCollection[0].Values, dataTable, 0, 1);
            }
            else 
            {
                run = false;
            }
        }

        protected void ShowFromCSV_1(object sender)
        {
            dataTable = BuildFromCSV(fileName_1);
            SentTabToMain(procNomber, dataTable);
        }

        protected void ShowFromCSV_2(object sender)
        {
            dataTable = BuildFromCSV(fileName_2);
            SentTabToMain(procNomber, dataTable);
        }

        protected DataTable BuildFromCSV(string fileName)
        {
            ChartValues.Clear();
            DataTable dt = CSV_DataTable.ConvertCSVtoDataTable(fileName);
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                ChartValues.Add(new MeasureModel
                {
                    ValueX = Convert.ToDouble(dt.Rows[i][0]),
                    ValueY = Convert.ToDouble(dt.Rows[i][1])
                });
            }
            return dt;
        }


        protected decimal k_Smooth;
        public decimal K_Smooth
        {
            get { return k_Smooth; }
            set { k_Smooth = value; }
        }

        protected int period_Smooth;
        public int Period_Smooth
        {
            get { return period_Smooth; }
            set { period_Smooth = value; }
        }


        protected bool movingAverage;
        public bool MovingAverage
        {
            get { return movingAverage; }
            set { movingAverage = value; }
        }



        protected void SmoothData()
        {
            //int period = 6;
            //double[] inputDoubles = new double[dataTable.Rows.Count];
            //for (int i = 1; i < dataTable.Rows.Count; i++)
            //{
            //    inputDoubles[i-1] = Convert.ToDouble(dataTable.Rows[i][1]);
            //}
            //IEnumerable<double> outputDoubles = inputDoubles.MovingAverage(period);


            //double[] arroutputDoubles = inputDoubles.MovingAverage(period).ToArray();
            //for (int i = 0; i < arroutputDoubles.Length; i++)
            //{
            //    dataTable.Rows[i][1] = arroutputDoubles[i].ToString();
            //}


            /////////////////////////////////////////////////////////////////////////////////////////
            ///


            if (MovingAverage)
            {
                double k = Convert.ToDouble(K_Smooth);
                double[] inputDoubles = new double[dataTable.Rows.Count];
                for (int i = 1; i < dataTable.Rows.Count; i++)
                {
                    inputDoubles[i - 1] = Convert.ToDouble(dataTable.Rows[i][1]);
                }
                double fOut = 0;
                double[] arroutputDoubles = new double[inputDoubles.Length];
                for (int i = 0; i < inputDoubles.Length - 1; i++)
                {
                    fOut = (1 - k) * inputDoubles[i] + k * fOut;
                    arroutputDoubles[i] = fOut;
                }
                for (int i = 1; i < arroutputDoubles.Length; i++)
                {
                    dataTable.Rows[i][1] = arroutputDoubles[i - 1].ToString();
                }
            }


            


            /////////////////////////////////////////////////////////////////////

            //double[] inputDoubles = new double[dataTable.Rows.Count];
            //for (int i = 1; i < dataTable.Rows.Count; i++)
            //{
            //    inputDoubles[i - 1] = Convert.ToDouble(dataTable.Rows[i][1]);
            //}
            //double[] arroutputDoubles = new double[inputDoubles.Length];
            //for (int i = 0; i < inputDoubles.Length - 1; i++)
            //{
            //    if (i >= Period_Smooth - 1)
            //    {
            //        double total = 0;
            //        for (int x = i; x > (i - Period_Smooth); x--)
            //            total += inputDoubles[x];
            //        double average = total / Period_Smooth;
            //        arroutputDoubles[i] = average;
            //    }

            //}
            //for (int i = 1; i < arroutputDoubles.Length; i++)
            //{
            //    dataTable.Rows[i][1] = arroutputDoubles[i - 1].ToString();
            //}


            //////////////////////////////////////////////////////////////////////////////////

            //var sums = new double[4];
            //var trendlinePoints = new List<double>();
            //var dataLen = dataTable.Rows.Count;

            //for (var i = 1; i < dataLen; i++)
            //{
            //    var logX = Math.Log(Convert.ToDouble(dataTable.Rows[i][0]));
            //    var logY = Math.Log(Convert.ToDouble(dataTable.Rows[i][1]));
            //    sums[0] += logX;
            //    sums[1] += logY;
            //    sums[2] += logX * logY;
            //    sums[3] += Math.Pow(logX, 2);
            //}

            //var b = (dataLen * sums[2] - sums[0] * sums[1]) / (dataLen * sums[3] - Math.Pow(sums[0], 2));
            //var a = Math.Pow(Math.E, (sums[1] - b * sums[0]) / dataLen);

            //for (int i = 1; i < dataTable.Rows.Count; i++)
            //{
            //    double pointY = a * Math.Pow(Convert.ToDouble(dataTable.Rows[i][0]), b);
            //    dataTable.Rows[i][1] = pointY.ToString();

            //}


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///

            else
            {
                double[] buffer = new double[Period_Smooth];
                double[] output = new double[dataTable.Rows.Count];
                int current_index = 0;
                for (int i = 1; i < dataTable.Rows.Count; i++)
                {
                    buffer[current_index] = Convert.ToDouble(dataTable.Rows[i][1]) / Period_Smooth;
                    double ma = 0.0;
                    for (int j = 0; j < Period_Smooth; j++)
                    {
                        ma += buffer[j];
                    }
                    output[i] = ma;
                    dataTable.Rows[i][1] = ma.ToString();
                    current_index = (current_index + 1) % Period_Smooth;
                }
            }
            
            
        }

        protected string fileName_1;
        protected string fileName_2;

        protected void OpenFile_1(object sender)
        {
            fileName_1 = GetFileName();
            dataTable = BuildFromCSV(fileName_1);
            SentTabToMain(procNomber, dataTable);
        }

        protected void OpenFile_2(object sender)
        {
            fileName_2 = GetFileName();
            dataTable = BuildFromCSV(fileName_2);
            SentTabToMain(procNomber, dataTable);
        }

        protected string GetFileName()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
                
            }
            return "";
        }


        private void BuildBack(string v)
        {
            SmoothData();
            ChartValues.Clear();
            try
            {
                for (int i = 1; i < dataTable.Rows.Count; i++)
                {
                    ChartValues.Add(new MeasureModel
                    {
                        ValueX = Convert.ToDouble(dataTable.Rows[i][0]),
                        ValueY = Convert.ToDouble(dataTable.Rows[i][1])
                    });
                }
                SentTabToMain(procNomber, dataTable);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }



        public virtual void GenerateReports(GenerateReportsMessage generate)
        {

        }

        protected void SentTabToMain(ushort procType, DataTable dataTable)
        {
            
            //if (ProcType_1 == procType)
            //{

                var generateReportsMessage = new SentDataTab(dataTable, procNomber);
                Messenger.Default.Send(generateReportsMessage);
            //}
        }


    }
}
