﻿using BelShina_HMI.Chart;
using BelShina_HMI.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;

namespace BelShina_HMI.ViewModels
{

    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class ConturViewModel_2 : GrafViewModel
    {
        public ConturViewModel_2(GrafSet grafSet, string cSvPath) : base(grafSet, cSvPath)
        { }

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Process.rLaserDistance_2")]
        public override float GetForse
        {
            get { return this._getForse; }
            set { this.SetProperty(ref this._getForse, value); }
        }
        private float _getForse;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.rLS_RealPos_2")]
        public override float ActualPosition
        {
            get
            {
                if ((FS_State != 0) && (FS_State < 6) && (FS_State > 3))
                {
                    Read("Application.HMI_Stepper.rLS_RealPos_2", "Application.HMI_Process.rLaserDistance_2");//  
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

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8100 PFC100 2ETH ECO.Application.HMI_Stepper.wLS_StepperState_2")]
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
