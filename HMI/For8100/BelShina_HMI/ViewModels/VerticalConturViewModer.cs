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

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
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
            ListOfItemsOPC listOfItemsOPC = new ListOfItemsOPC();
            OPC_UA = new OPC_UA_Client("192.168.1.17", 500d, listOfItemsOPC.GetOPCitems());
            _ = Task();
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
            //MessageBox.Show(start.ToString() + " ; " + dataTable.Rows.Count.ToString());
            if (start && ValuesAfter1.Count > 2)
            {
                string year = System.DateTime.Now.Year.ToString();
                string month = System.DateTime.Now.Month.ToString();
                string day = System.DateTime.Now.Day.ToString();
                string hour = System.DateTime.Now.Hour.ToString();
                string minute = System.DateTime.Now.Minute.ToString();
                string path = @"D:\Протоколы\" + name + @"\";
                System.IO.Directory.CreateDirectory(path);
                ReadWriteCSV readWriteCSV = new ReadWriteCSV(path + day + "_" + month + "_" + year + "_" + hour + "_" + minute + "_" + name + ".csv");
                readWriteCSV.WriteToCSV("Height", column1, column2, column3, column4);
                //MessageBox.Show(ValuesBefore1.Count.ToString());
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

    }
}
