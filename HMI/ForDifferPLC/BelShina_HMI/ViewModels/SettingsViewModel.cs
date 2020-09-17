using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class SettingsViewModel : SubscriptionBase
    {
        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_RoughApprox")]
        public ushort RoughApprox
        {
            get 
            {
                
                return this.roughApprox; 
            }
            set 
            {              
                this.SetProperty(ref this.roughApprox, value);                
            }
        }

        private ushort roughApprox;



        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_ActualPos")]
        public long ActualPosition
        {
            get 
            {
                Test++;
                return this.actualPosition; 
            }
            set { this.SetProperty(ref this.actualPosition, value); }
        }

        private long actualPosition;

        public int Test
        {
            get { return this.test; }
            set { this.SetProperty(ref this.test, value); }
        }

        private int test;



        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_Step")]
        public ushort Step
        {
            get { return this.step; }
            set { this.SetProperty(ref this.step, value); }
        }

        private ushort step;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_Sensativity")]
        public float Sensativity
        {
            get { return this.sensativity; }
            set { this.SetProperty(ref this.sensativity, value); }
        }

        private float sensativity;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_SetSpeed")]
        public ushort SetSpeed
        {
            get { return this.setSpeed; }
            set { this.SetProperty(ref this.setSpeed, value); }
        }

        private ushort setSpeed;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_SetAcceleration")]
        public ushort SetAcceleration
        {
            get { return this.setAcceleration; }
            set { this.SetProperty(ref this.setAcceleration, value); }
        }

        private ushort setAcceleration;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_SetDeceleration")]
        public ushort SetDeceleration
        {
            get { return this.setDeceleration; }
            set { this.SetProperty(ref this.setDeceleration, value); }
        }

        private ushort setDeceleration;
    }
}
