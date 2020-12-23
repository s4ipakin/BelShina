using BelShina_HMI.Chart;
using BelShina_HMI.Reports;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 2)]
    public class LineForceGrafViewModel_2 : GrafViewModel
    {
        public LineForceGrafViewModel_2(GrafSet grafSet, string cSvPath) : base(grafSet, cSvPath)
        { }

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.rFS_GetForce")]
        public override float GetForse
        {
            get { return this._getForse; }
            set { this.SetProperty(ref this._getForse, value); }
        }
        private float _getForse;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rDistance_2")]
        public override float ActualPosition
        {
            get 
            {
                if ((FS_State != 0) && (FS_State < 5) && (wProcType_1 == 3) /*&& (this._actualPosition > previousPosition)*/)
                {
                    //Read("Application.HMI_Process.rDistance_2", "Application.HMI_Stepper.rFS_GetForce");//  
                    //GetGrafPoints();
                    //float force = DistanceForce / 10000;
                    //grafValueY = force.ToString();
                    //float distance = ((float)DistanceForce - (force * 10000)) / 100;
                    //grafValueX = distance.ToString();
                    //GetGrafPoints();
                }
                else
                {
                    run = false;
                }
                
                return this._actualPosition;
            }
            set { this.SetProperty(ref this._actualPosition, value); }
        }
        private float _actualPosition;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.wFS_State")]
        public override ushort FS_State
        {
            get
            {
                if (this.lS_State == 5)
                {
                    if (!send)
                    {
                        send = true;
                        //SaveToCSV(true, cSvPath, "Distance", "Force");
                        //SentTabToMain(3);
                    }
                }
                else
                {
                    send = false;
                }
                if (!this.start)
                {

                }
                return this.lS_State;
            }
            set { this.SetProperty(ref this.lS_State, value); }
        }

        private bool send = false;
        

        private ushort lS_State;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.wProcType")]
        public override ushort ProcType_1
        {
            get { return this.wProcType_1; }
            set { this.SetProperty(ref this.wProcType_1, value); }
        }

        private ushort wProcType_1;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.xFS_Start")]
        public override bool Start
        {
            get
            {
                if (this.start)
                {
                    xStarted = true;
                }
                else if (xStarted)
                {
                    xStarted = false;
                    if (ProcType_1 == 3)
                    {
                        SaveToCSV(true, cSvPath, "Distance", "Force");
                        SentTabToMain(3);
                    }
                }
                if (!this.start)
                {
                    previousPosition = 0f;
                }
                return this.start;
            }
            set { this.SetProperty(ref this.start, value); }
        }
        private bool start;

        private bool xStarted = false;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.diDistanceForce")]
        public override int DistanceForce
        {
            get
            {
                if ((wProcType_1 == 3) && (FS_State != 0) && (FS_State < 5))
                {
                    float force = this.distanceForce / 10000;
                    grafValueY = force.ToString();
                    float distance = ((float)this.distanceForce - (force * 10000)) / 100;
                    //distance =(float)Math.Round(Convert.ToDecimal(distance), 1);
                    grafValueX = distance.ToString();
                    
                    if (distance > previousPosition)
                    {
                        previousPosition = distance;
                        GetGrafPoints();
                    }
                    
                }
                
                return this.distanceForce;
            }
            set { this.SetProperty(ref this.distanceForce, value); }
        }
        private int distanceForce;


        public override void GenerateReports(GenerateReportsMessage generate)
        {
            //SaveToCSV(true, cSvPath, "Distance", "Force");
        }
    }
}
