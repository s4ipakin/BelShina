﻿using BelShina_HMI.Chart;
using BelShina_HMI.Reports;
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
    public class ConturViewModel_1 : GrafViewModel
    {
        public ConturViewModel_1(GrafSet grafSet, string cSvPath) : base(grafSet, cSvPath)
        { }

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rLaserDistance_1")]
        public override float GetForse
        {
            get { return this._getForse; }
            set { this.SetProperty(ref this._getForse, value); }
        }
        private float _getForse;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rLS_RealPos_1")]
        public override float ActualPosition
        {
            get 
            {
                
                if ((FS_State != 0) && (FS_State < 7) && (FS_State > 4))
                {
                    
                    Read("Application.HMI_Stepper.rLS_RealPos_1", "Application.HMI_Process.rLaserDistance_1");//  
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


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wLS_StepperState_1")]
        public override ushort FS_State
        {
            get { return this.lS_State; }
            set { this.SetProperty(ref this.lS_State, value); }
        }
        private ushort lS_State;

        public override void GenerateReports(GenerateReportsMessage generate)
        {
            SaveToCSV(true, cSvPath, "Distance", "LaserData");
        }

        
    }
}
