using BelShina_HMI.Chart;
using BelShina_HMI.Reports;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Workstation.ServiceModel.Ua;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class LineForceGrafViewModel_1 : GrafViewModel
    {
        public LineForceGrafViewModel_1(GrafSet grafSet, string cSvPath) : base(grafSet, cSvPath)
        { }

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.rFS_GetForce")]
        public override float GetForse
        {
            get { return this._getForse;}
            set { this.SetProperty(ref this._getForse, value); }
        }
        private float _getForse;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rDistance_1")]
        public override float ActualPosition
        {
            get 
            {
                
                if ((FS_State != 0) && (FS_State < 5))
                {
                    
                    Read("Application.HMI_Process.rDistance_1", "Application.HMI_Stepper.rFS_GetForce");//  
                    GetGrafPoints();
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


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.wProcType")]
        public override ushort ProcType_1
        {
            get { return this.wProcType_1; }
            set { this.SetProperty(ref this.wProcType_1, value); }
        }

        private ushort wProcType_1;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.wFS_State")]
        public override ushort FS_State
        {
            get
            {
                if (this.lS_State == 5)
                {
                    //MessageBox.Show("Application.HMI_Stepper.rLS_RealPos_1");
                    if (!send)
                    {
                        send = true;
                        //SaveToCSV(true, cSvPath, "Distance", "Force");
                        //SentTabToMain(2);
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
                    if (ProcType_1 == 2)
                    {
                        SaveToCSV(true, cSvPath, "Distance", "Force");
                        SentTabToMain(2);
                    }
                    
                }
                return this.start; 
            }
            set { this.SetProperty(ref this.start, value); }
        }
        private bool start;

        private bool xStarted = false;

        public override void GenerateReports(GenerateReportsMessage generate)
        {
            //MessageBox.Show("generate");
            //SaveToCSV(true, cSvPath, "Distance", "Force");
        }
    }
}
