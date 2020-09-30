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

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class VerticalConturViewModer : SubscriptionBase
    {
        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wST_State_1")]
        public ushort ST_State_1
        {
            get 
            { 
                
                return this.gT_State_1; 
            }
            set { this.SetProperty(ref this.gT_State_1, value); }
        }
        private ushort gT_State_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wGS_State_1")]
        public ushort GS_State_1
        {
            get
            {

                return this.gS_State_1;
            }
            set { this.SetProperty(ref this.gS_State_1, value); }
        }
        private ushort gS_State_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rLS_RealPos_1")]
        public float LS_RealPos_1
        {
            get { return this.lS_RealPos_1; }
            set { this.SetProperty(ref this.lS_RealPos_1, value); }
        }
        private float lS_RealPos_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rLS_RealPos_2")]
        public float LS_RealPos_2
        {
            get { return this.lS_RealPos_2; }
            set { this.SetProperty(ref this.lS_RealPos_2, value); }
        }
        private float lS_RealPos_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.xReadOPC_1")]
        public bool ReadOPC_1
        {
            get 
            { 
                if (this.readOPC_1)
                {
                    SetValues(ST_State_1, ValuesBefore1, ValuesAfter1, LS_RealPos_1);
                    WriteBool("Application.HMI_Process.xReadOPC_1", false);
                }
                return this.readOPC_1; 
            }
            set { this.SetProperty(ref this.readOPC_1, value); }
        }
        private bool readOPC_1;



        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.xReadOPC_2")]
        public bool ReadOPC_2
        {
            get
            {
                if (this.readOPC_2)
                {
                    SetValues(ST_State_1, ValuesBefore2, ValuesAfter2, LS_RealPos_2);
                    WriteBool("Application.HMI_Process.xReadOPC_2", false);
                }
                return this.readOPC_2;
            }
            set { this.SetProperty(ref this.readOPC_2, value); }
        }
        private bool readOPC_2;




        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.xCurveStarted_1")]
        public bool CurveStarted_1
        {
            get
            {
                if (this.curveStarted_1)
                {
                    ClearCurves(ST_State_1, ValuesBefore1, ValuesAfter1);
                    WriteBool("Application.HMI_Process.xCurveStarted_1", false);
                }
                return this.curveStarted_1;
            }
            set { this.SetProperty(ref this.curveStarted_1, value); }
        }
        private bool curveStarted_1;

        


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.xCurveStarted_2")]
        public bool CurveStarted_2
        {
            get 
            {
                if (this.curveStarted_2)
                {
                    ClearCurves(ST_State_1, ValuesBefore2, ValuesAfter2);
                    WriteBool("Application.HMI_Process.xCurveStarted_2", false);
                }
                return this.curveStarted_2; 
            }
            set { this.SetProperty(ref this.curveStarted_2, value); }
        }
        private bool curveStarted_2;


        

        protected int run1 = 0;
        protected int run2 = 0;


        public ChartValues<double> ValuesBefore1 { get; set; }
        public ChartValues<double> ValuesBefore2 { get; set; }
        public ChartValues<double> ValuesAfter1 { get; set; }
        public ChartValues<double> ValuesAfter2 { get; set; }


        //ListOfItemsOPC listOfItemsOPC = new ListOfItemsOPC();
        protected OPC_UA_Client OPC_UA;
        //OPC_UA = new OPC_UA_Client("192.168.1.17", 2000d, listOfItemsOPC.GetOPCitems());
        protected Dictionary<string, string> itemDict = new Dictionary<string, string>();
        

        public VerticalConturViewModer()
        {
            ValuesBefore1 = new ChartValues<double>();
            ValuesBefore2 = new ChartValues<double>();
            ValuesAfter1 = new ChartValues<double>();
            ValuesAfter2 = new ChartValues<double>();
            ListOfItemsOPC listOfItemsOPC = new ListOfItemsOPC();
            OPC_UA = new OPC_UA_Client("192.168.1.17", 500d, listOfItemsOPC.GetOPCitems());
            _ = Task();
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

        protected void ClearCurves(ushort procState, ChartValues<double> valuesBefore, ChartValues<double> valuesAfter)
        {           
            switch(procState)
            {
                case 4:
                    valuesAfter.Clear();
                    break;
                case 8:
                    valuesBefore.Clear();
                    break;
                default:
                    valuesAfter.Clear();
                    valuesBefore.Clear();
                    break;
            }

        }

        protected void SetValues(ushort procState, ChartValues<double> valuesBefore, ChartValues<double> valuesAfter, float value)
        {
            double dValue = Convert.ToDouble(value);
            if (procState == 4)
            {
                valuesAfter.Add(dValue);
            }
            else
            {
                valuesBefore.Add(dValue);
            }
        }

    }
}
