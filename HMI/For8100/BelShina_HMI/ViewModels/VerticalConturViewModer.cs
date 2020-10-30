using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;
using BelShina_HMI.Chart;
using BelShina_HMI.OPC;
using LiveCharts;
using System.Windows;
using BelShina_HMI.Reports;
using System.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 2)]
    public class VerticalConturViewModer : SubscriptionBase
    {
        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.wST_State_1")]
        public ushort ST_State_1
        {
            get 
            {
                //MessageBox.Show(this.gT_State_1.ToString());
                if ((gT_State_1 == 5)&&(ValuesBefore1.Count > 0) && (ValuesBefore2.Count > 0)
                    && (ValuesAfter1.Count > 0) && (ValuesAfter2.Count > 0))
                {
                    SaveToCSV(true, "Профиль", "Before Laser 1", "Before Laser 2", "After Laser 1", "After Laser 2");
                }
                if (gT_State_1 == 4)
                {
                    doAfter = true;
                }
                if (gT_State_1 == 8)
                {
                    doAfter = false;
                }
                    return this.gT_State_1; 
            }
            set { this.SetProperty(ref this.gT_State_1, value); }
        }
        private ushort gT_State_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.wGS_State_1")]
        public ushort GS_State_1
        {
            get
            {

                return this.gS_State_1;
            }
            set { this.SetProperty(ref this.gS_State_1, value); }
        }
        private ushort gS_State_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rLaserDistance_1")]
        public float LaserDistance_1
        {
            get { return this.laserDistance_1; }
            set { this.SetProperty(ref this.laserDistance_1, value); }
        }
        private float laserDistance_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rLaserDistance_2")]
        public float LaserDistance_2
        {
            get { return this.laserDistance_2; }
            set { this.SetProperty(ref this.laserDistance_2, value); }
        }
        private float laserDistance_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.rLS_RealPos_1")]
        public float LS_RealPos_1
        {
            get { return this.lS_RealPos_1; }
            set { this.SetProperty(ref this.lS_RealPos_1, value); }
        }
        private float lS_RealPos_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.rLS_RealPos_2")]
        public float LS_RealPos_2
        {
            get { return this.lS_RealPos_2; }
            set { this.SetProperty(ref this.lS_RealPos_2, value); }
        }
        private float lS_RealPos_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.xReadOPC_1")]
        public bool ReadOPC_1
        {
            get 
            { 
                if (this.readOPC_1)
                {
                    if (/*conturApprox_1.Check(lS_RealPos_1, LaserDistance_1)*/true)
                    {
                        List<ValuePair> values = new List<ValuePair>();
                        //values = GetApproxValues("Application.HMI_Process.rStepPos_1", "Application.HMI_Process.rLaserData_1", conturApprox_1);
                        values = conturApprox_1.GetPoints(StepPos_1, LaserData_1);
                        
                        SetValues(ValuesBefore1, ValuesAfter1, ChartValuesBefore1, ChartValuesAfter1, values);
                        WriteBool("Application.HMI_Process.xReadOPC_1", false);
                    }
                    
                }
                return this.readOPC_1; 
            }
            set { this.SetProperty(ref this.readOPC_1, value); }
        }
        private bool readOPC_1;



        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.xReadOPC_2")]
        public bool ReadOPC_2
        {
            get
            {
                if (this.readOPC_2)
                {
                    if(/*conturApprox_2.Check(lS_RealPos_2, LaserDistance_2)*/true)
                    {
                        List<ValuePair> values = new List<ValuePair>();
                        //values = GetApproxValues("Application.HMI_Process.rStepPos_2", "Application.HMI_Process.rLaserData_2", conturApprox_2);
                        values = conturApprox_2.GetPoints(StepPos_2, LaserData_2);
                        SetValues(ValuesBefore2, ValuesAfter2, ChartValuesBefore2, ChartValuesAfter2, values);
                        WriteBool("Application.HMI_Process.xReadOPC_2", false);
                    }
                    
                }
                return this.readOPC_2;
            }
            set { this.SetProperty(ref this.readOPC_2, value); }
        }
        private bool readOPC_2;




        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.xCurveStarted_1")]
        public bool CurveStarted_1
        {
            get
            {
                if (this.curveStarted_1)
                {
                    conturApprox_1.Reset();
                    ClearCurves(ST_State_1, ValuesBefore1, ValuesAfter1, ChartValuesBefore1, ChartValuesAfter1);
                    WriteBool("Application.HMI_Process.xCurveStarted_1", false);
                }
                return this.curveStarted_1;
            }
            set { this.SetProperty(ref this.curveStarted_1, value); }
        }
        private bool curveStarted_1;

        


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.xCurveStarted_2")]
        public bool CurveStarted_2
        {
            get 
            {
                if (this.curveStarted_2)
                {
                    conturApprox_2.Reset();
                    ClearCurves(ST_State_1, ValuesBefore2, ValuesAfter2, ChartValuesBefore2, ChartValuesAfter2);
                    WriteBool("Application.HMI_Process.xCurveStarted_2", false);
                }
                return this.curveStarted_2; 
            }
            set { this.SetProperty(ref this.curveStarted_2, value); }
        }
        private bool curveStarted_2;



        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rStepPos_1")]
        public float StepPos_1
        {
            get { return this.stepPos_1; }
            set { this.SetProperty(ref this.stepPos_1, value); }
        }
        private float stepPos_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rStepPos_2")]
        public float StepPos_2
        {
            get { return this.stepPos_2; }
            set { this.SetProperty(ref this.stepPos_2, value); }
        }
        private float stepPos_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rLaserData_1")]
        public float LaserData_1
        {
            get { return this.laserData_1; }
            set { this.SetProperty(ref this.laserData_1, value); }
        }
        private float laserData_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rLaserData_2")]
        public float LaserData_2
        {
            get { return this.laserData_2; }
            set { this.SetProperty(ref this.laserData_2, value); }
        }
        private float laserData_2;


        protected int run1 = 0;
        protected int run2 = 0;


        public ChartValues<double> ValuesBefore1 { get; set; }
        public ChartValues<double> ValuesBefore2 { get; set; }
        public ChartValues<double> ValuesAfter1 { get; set; }
        public ChartValues<double> ValuesAfter2 { get; set; }

        public ChartValues<MeasureModel> ChartValuesBefore1 { get; set; }
        public ChartValues<MeasureModel> ChartValuesBefore2 { get; set; }
        public ChartValues<MeasureModel> ChartValuesAfter1 { get; set; }
        public ChartValues<MeasureModel> ChartValuesAfter2 { get; set; }

        public ICommand ButtonChooseFile_1 { get; set; }
        public ICommand ButtonBuidGraf_1 { get; set; }
        public ICommand ButtonChooseFile_2 { get; set; }
        public ICommand ButtonBuidGraf_2 { get; set; }
        public ICommand ButtonBuildBack { get; set; }
        //public ChartValues<MeasureModel> ChartValues { get; set; }


        //ListOfItemsOPC listOfItemsOPC = new ListOfItemsOPC();
        protected OPC_UA_Client OPC_UA;
        //OPC_UA = new OPC_UA_Client("192.168.1.17", 2000d, listOfItemsOPC.GetOPCitems());
        protected Dictionary<string, string> itemDict = new Dictionary<string, string>();
        
        ConturGrafSet conturGrafSet;
        ConturApprox conturApprox_1;
        ConturApprox conturApprox_2;
        public VerticalConturViewModer()
        {
            ValuesBefore1 = new ChartValues<double>();
            ValuesBefore2 = new ChartValues<double>();
            ValuesAfter1 = new ChartValues<double>();
            ValuesAfter2 = new ChartValues<double>();
            ChartValuesBefore1 = new ChartValues<MeasureModel>();
            ChartValuesBefore2 = new ChartValues<MeasureModel>();
            ChartValuesAfter1 = new ChartValues<MeasureModel>();
            ChartValuesAfter2 = new ChartValues<MeasureModel>();
            //ChartValues = new ChartValues<MeasureModel>();
            conturGrafSet = new ConturGrafSet();
            YaxesName = conturGrafSet.yAxesName;
            XaxesName = conturGrafSet.xAxesName;
            conturApprox_1 = new ConturApprox();
            conturApprox_2 = new ConturApprox();
            ButtonChooseFile_1 = new RelayCommand(o => OpenFile_1("ReportsButton"));
            ButtonBuidGraf_1 = new RelayCommand(o => BuildFronCSV_1("ReportsButton"));
            ButtonChooseFile_2 = new RelayCommand(o => OpenFile_2("ReportsButton"));
            ButtonBuidGraf_2 = new RelayCommand(o => BuildFronCSV_2("ReportsButton"));
            ButtonBuildBack = new RelayCommand(o => BuildBack("ReportsButton"));
            Messenger.Default.Register<SentModelName>(this, SetFileNameEnding);
            ListOfItemsOPC listOfItemsOPC = new ListOfItemsOPC();
            OPC_UA = new OPC_UA_Client("192.168.1.17", 500d, listOfItemsOPC.GetOPCitems());
            _ = Task();
        }

        protected string fileEnding = "";

        protected void SetFileNameEnding(SentModelName obj)
        {
            fileEnding = obj.ModelName;
        }

        protected string yaxesName;
        public string YaxesName
        {
            get { return yaxesName; }
            set { yaxesName = value; }
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

        protected async void SetValue(string name, ChartValues<double> value)
        {
            if (OPC_UA != null)
            {
                try
                {
                    //MessageBox.Show("get");
                    itemDict = await OPC_UA.ReadOPCAsync();
                    double grafValueX =Convert.ToDouble(itemDict[name]);
                    value.Add(grafValueX);
                }
                catch (Exception ex) { }
            }
        }


        protected async void Read( )
        {
            if (OPC_UA != null)
            {
                try
                {
                    //MessageBox.Show("get");
                    itemDict = await OPC_UA.ReadOPCAsync();
                    
                }
                catch (Exception ex) { }
            }
        }

        protected async void WriteBool(string name, bool value)
        {
            if (OPC_UA != null)
            {
                try
                {
                    //MessageBox.Show("get");
                    await OPC_UA.WriteBool(name, value);
                }
                catch(Exception ex) { }
            }
        }

        protected void ClearCurves(ushort procState, ChartValues<double> valuesBefore, ChartValues<double> valuesAfter, ChartValues<MeasureModel> chartValuesBefore, ChartValues<MeasureModel> chartValuesAfter)
        {           
            switch(procState)
            {
                //case 1:
                case 3:
                case 4:
                    valuesAfter.Clear();
                    chartValuesAfter.Clear();
                    break;
                case 9:
                case 10:
                    break;
                case 8:
                    valuesBefore.Clear();
                    chartValuesBefore.Clear();
                    break;
                default:
                    valuesAfter.Clear();
                    valuesBefore.Clear();
                    chartValuesAfter.Clear();
                    chartValuesBefore.Clear();
                    break;
            }

        }
        bool doAfter = false;
        

        protected void SetValues(ChartValues<double> valuesBefore, ChartValues<double> valuesAfter, ChartValues<MeasureModel> chartValuesBefore,
                                    ChartValues<MeasureModel> chartValuesAfter, List<ValuePair> approxValues)
        {
            
            
            if (!doAfter)
            {
                for (int i = 0; i < approxValues.Count; i++)
                {
                    valuesBefore.Add(approxValues[i].LaserDistance);
                    
                }
                //chartValuesBefore.Add(approxValues[approxValues.Count - 1].LaserDistance);
                if (approxValues.Count > 0)
                {
                    chartValuesBefore.Add(new MeasureModel
                    {
                        ValueX = Convert.ToDouble(approxValues[approxValues.Count - 1].LaserDistance),
                        ValueY = Convert.ToDouble(approxValues[approxValues.Count - 1].StepDistance)
                    });
                }
                

            }
            else 
            {
                for (int i = 0; i < approxValues.Count; i++)
                {
                    valuesAfter.Add(approxValues[i].LaserDistance);
                }
                //chartValuesAfter.Add(approxValues[approxValues.Count - 1].LaserDistance);
                if (approxValues.Count > 0)
                {
                    chartValuesAfter.Add(new MeasureModel
                    {
                        ValueX = Convert.ToDouble(approxValues[approxValues.Count - 1].LaserDistance),
                        ValueY = Convert.ToDouble(approxValues[approxValues.Count - 1].StepDistance)
                    });
                }
                
            }
        }

        private List<ValuePair> GetApproxValues(string nameStepPos, string nameLaserPos, ConturApprox conturApprox)
        {
            List<ValuePair> values = new List<ValuePair>();
            Read();
            //MessageBox.Show(itemDict.Count.ToString());
            try
            {
                float stepperPos = (float)(Convert.ToDouble(itemDict[nameStepPos]));
                float laserDist = (float)(Convert.ToDouble(itemDict[nameLaserPos]));
                values = conturApprox.GetPoints(stepperPos, laserDist);
            }
            catch (Exception ex) { }
            
            return values;
        }


        protected void SaveToCSV(bool start, string name, string column1, string column2, string column3, string column4)
        {
            if (start && ValuesAfter1.Count > 2)
            {
                ReadWriteCSV readWriteCSV = new ReadWriteCSV(CreatePath.CreateReportPath(name, fileEnding));
                readWriteCSV.WriteToCSV("Height", column1, column2, column3, column4);
                for (int i = 0; i < ValuesBefore1.Count; i++)
                {
                    if ((i < ValuesAfter1.Count))
                    {
                        readWriteCSV.WriteToCSV(i.ToString(), ValuesBefore1[i].ToString(), ValuesBefore2[i].ToString(), ValuesAfter1[i].ToString(), ValuesAfter2[i].ToString());
                    }
                    else
                    {
                        readWriteCSV.WriteToCSV(i.ToString(), ValuesBefore1[i].ToString(), ValuesBefore2[i].ToString());
                    }
                    
                }
            }
        }


        protected string fileName_1;
        protected string fileName_2;

        protected void OpenFile_1(object sender)
        {
            fileName_1 = GetFileName();
            BuildFromCSV(fileName_1);
        }

        protected void OpenFile_2(object sender)
        {
            fileName_2 = GetFileName();
            BuildFromCSV(fileName_2);
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

        protected void BuildFronCSV_1(object sender)
        {
            BuildFromCSV(fileName_1);
        }

        protected void BuildFronCSV_2(object sender)
        {
            BuildFromCSV(fileName_2);
        }

        protected void BuildFromCSV(string fileName)
        {
            ChartValuesBefore1.Clear();
            ChartValuesBefore2.Clear();
            ChartValuesAfter1.Clear();
            ChartValuesAfter2.Clear();
            DataTable dt = CSV_DataTable.ConvertCSVtoDataTable(fileName);
            int step = 1;
            if (dt.Rows.Count > 100)
            {
                step = dt.Rows.Count / 100;
            }
            try
            {
                for (int i = 1; i < dt.Rows.Count; i = i + step)
                {
                    if (dt.Rows[i][1].ToString() != "")
                    {
                        ChartValuesBefore1.Add(new MeasureModel
                        {
                            ValueX = Convert.ToDouble(dt.Rows[i][1]),
                            ValueY = Convert.ToDouble(dt.Rows[i][0])
                        });

                        ChartValuesAfter1.Add(new MeasureModel
                        {
                            ValueX = Convert.ToDouble(dt.Rows[i][2]),
                            ValueY = Convert.ToDouble(dt.Rows[i][0])
                        });
                    }
                    if (dt.Rows[i][3].ToString() != "")
                    {
                        ChartValuesBefore2.Add(new MeasureModel
                        {
                            ValueX = Convert.ToDouble(dt.Rows[i][3]),
                            ValueY = Convert.ToDouble(dt.Rows[i][0])
                        });

                        ChartValuesAfter2.Add(new MeasureModel
                        {
                            ValueX = Convert.ToDouble(dt.Rows[i][4]),
                            ValueY = Convert.ToDouble(dt.Rows[i][0])
                        });
                    }
                    //MessageBox.Show(i.ToString());
                }
                
            }
            
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void BuildBack(string v)
        {
            ChartValuesBefore1.Clear();
            ChartValuesBefore2.Clear();
            ChartValuesAfter1.Clear();
            ChartValuesAfter2.Clear();
            int step = ValuesBefore1.Count / 100;
            for (int i = 0; i < ValuesBefore1.Count; i = i + step)
            {
                ChartValuesBefore1.Add(new MeasureModel
                {
                    ValueX = ValuesBefore1[i],
                    ValueY = Convert.ToDouble(i)
                });

                ChartValuesBefore2.Add(new MeasureModel
                {
                    ValueX = ValuesBefore2[i],
                    ValueY = Convert.ToDouble(i)
                });
            }

            for (int i = 0; i < ValuesAfter1.Count; i = i + step)
            {
                ChartValuesAfter1.Add(new MeasureModel
                {
                    ValueX = ValuesAfter1[i],
                    ValueY = Convert.ToDouble(i)
                });

                ChartValuesAfter2.Add(new MeasureModel
                {
                    ValueX = ValuesAfter2[i],
                    ValueY = Convert.ToDouble(i)
                });
            }
        }


    }
}
