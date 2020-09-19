﻿using BelShina_HMI.Chart;
using BelShina_HMI.Reports;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class LineForceGrafViewModel_2 : GrafViewModel
    {
        public LineForceGrafViewModel_2(GrafSet grafSet, string cSvPath) : base(grafSet, cSvPath)
        { }

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_GetForce")]
        public override float GetForse
        {
            get { return this._getForse; }
            set { this.SetProperty(ref this._getForse, value); }
        }
        private float _getForse;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_LinedWay_2")]
        public override float ActualPosition
        {
            get 
            {
                Read("Application.HMI_Stepper.rFS_LinedWay_2", "Application.HMI_Stepper.rFS_GetForce");//  
                GetGrafPoints();
                return this._actualPosition;
            }
            set { this.SetProperty(ref this._actualPosition, value); }
        }
        private float _actualPosition;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.xProcFinished")]
        public override bool ProcFinished
        {
            get
            {
                SaveToCSV(this.procFinished, cSvPath, "Distance", "Force");
                return this.procFinished;
            }
            set { this.SetProperty(ref this.procFinished, value); }
        }
    }
}
