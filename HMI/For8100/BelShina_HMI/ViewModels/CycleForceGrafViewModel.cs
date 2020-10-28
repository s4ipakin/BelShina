using BelShina_HMI.Chart;
using BelShina_HMI.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;
using System.Windows;
using BelShina_HMI.OPC;
using GalaSoft.MvvmLight.Messaging;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 2)]
    public class CycleForceGrafViewModel : GrafViewModel 
    {
        //OPC_UA_Client OPC_UA;
        public CycleForceGrafViewModel(GrafSet grafSet, string cSvPath) : base(grafSet, cSvPath)
        {
            
        }

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_GetForce")]
        public override float GetForse
        {
            get { return this._getForse; } 
            set { this.SetProperty(ref this._getForse, value); }
        }
        private float _getForse;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_CycledWay")]
        public override float ActualPosition
        {
            get
            {
                if ((FS_State != 0) && (FS_State < 5))
                {
                    //Read("Application.HMI_Stepper.rFS_CycledWay", "Application.HMI_Stepper.rFS_GetForce");//  
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



        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wProcType")]
        public override ushort ProcType_1
        {
            get { return this.wProcType_1; }
            set { this.SetProperty(ref this.wProcType_1, value); }
        }

        private ushort wProcType_1;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_State")]
        public override ushort FS_State
        {
            get
            {
                if (this.lS_State == 5)
                {
                    if (!send)
                    {
                        send = true;
                        //SaveToCSV(true, cSvPath, "Angle", "Force");
                        //SentTabToMain(1);
                    }
                }
                else
                {
                    send = false;
                }

                return this.lS_State;
            }
            set { this.SetProperty(ref this.lS_State, value); }
        }

        private bool send = false;
        

        private ushort lS_State;
        private float _actualPosition;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.xProcFinished")]
        public override bool ProcFinished
        {
            get{return false;}
            set { this.SetProperty(ref this.procFinished, value); }
        }

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.xFS_Start")]
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
                    if (ProcType_1 == 1)
                    {
                        SaveToCSV(true, cSvPath, "Angle", "Force");
                        SentTabToMain(1);
                    }
                        
                }
                return this.start;
            }
            set { this.SetProperty(ref this.start, value); }
        }
        private bool start;

        private bool xStarted = false;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.diDistanceForce")]
        public override int DistanceForce
        {
            get
            {
                if (wProcType_1 == 1)
                {
                    float force = this.distanceForce / 10000;
                    grafValueY = force.ToString();
                    float distance = ((float)this.distanceForce - (force * 10000)) / 10;
                    grafValueX = distance.ToString();
                    GetGrafPoints();
                }
                return this.distanceForce;
            }
            set { this.SetProperty(ref this.distanceForce, value); }
        }
        private int distanceForce;

        public override void GenerateReports(GenerateReportsMessage generate)
        {
            //SaveToCSV(true, cSvPath, "Angle", "Force");
        }




    }
}
