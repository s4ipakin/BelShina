﻿using System;
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


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wLS_SetSpeed_1")]
        public ushort LS_SetSpeed_1
        {
            get { return this.lS_SetSpeed_1; }
            set { this.SetProperty(ref this.lS_SetSpeed_1, value); }
        }

        private ushort lS_SetSpeed_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wLS_Acceleration_1")]
        public ushort LS_Acceleration_1
        {
            get { return this.lS_Acceleration_1; }
            set { this.SetProperty(ref this.lS_Acceleration_1, value); }
        }

        private ushort lS_Acceleration_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wLS_Deceleration_1")]
        public ushort LS_Deceleration_1
        {
            get { return this.lS_Deceleration_1; }
            set { this.SetProperty(ref this.lS_Deceleration_1, value); }
        }

        private ushort lS_Deceleration_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wLS_SetSpeed_2")]
        public ushort LS_SetSpeed_2
        {
            get { return this.lS_SetSpeed_2; }
            set { this.SetProperty(ref this.lS_SetSpeed_2, value); }
        }

        private ushort lS_SetSpeed_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wLS_Acceleration_2")]
        public ushort LS_Acceleration_2
        {
            get { return this.lS_Acceleration_2; }
            set { this.SetProperty(ref this.lS_Acceleration_2, value); }
        }

        private ushort lS_Acceleration_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wLS_Deceleration_2")]
        public ushort LS_Deceleration_2
        {
            get { return this.lS_Deceleration_2; }
            set { this.SetProperty(ref this.lS_Deceleration_2, value); }
        }

        private ushort lS_Deceleration_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.k_1")]
        public float K_1
        {
            get { return this.k_1; }
            set { this.SetProperty(ref this.k_1, value); }
        }

        private float k_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.k_2")]
        public float K_2
        {
            get { return this.k_2; }
            set { this.SetProperty(ref this.k_2, value); }
        }

        private float k_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.k_3")]
        public float K_3
        {
            get { return this.k_3; }
            set { this.SetProperty(ref this.k_3, value); }
        }

        private float k_3;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.k_4")]
        public float K_4
        {
            get { return this.k_4; }
            set { this.SetProperty(ref this.k_4, value); }
        }

        private float k_4;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rEncoderImpulseCount")]
        public ushort EncoderImpulseCount
        {
            get { return this.rEncoderImpulseCount; }
            set { this.SetProperty(ref this.rEncoderImpulseCount, value); }
        }
        private ushort rEncoderImpulseCount;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rKforce_1")]
        public float Kforce_1
        {
            get { return this.kforce_1; }
            set { this.SetProperty(ref this.kforce_1, value); }
        }
        private float kforce_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rKforce_2")]
        public float Kforce_2
        {
            get { return this.kforce_2; }
            set { this.SetProperty(ref this.kforce_2, value); }
        }
        private float kforce_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rKforce_Ctcle")]
        public float Kforce_Ctcle
        {
            get { return this.kForce_Ctcle; }
            set { this.SetProperty(ref this.kForce_Ctcle, value); }
        }
        private float kForce_Ctcle;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rRadius")]
        public float Radius
        {
            get { return this.radius; }
            set { this.SetProperty(ref this.radius, value); }
        }
        private float radius;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.StepAngle_1")]
        public float StepAngle_1
        {
            get { return this.stepAngle_1; }
            set { this.SetProperty(ref this.stepAngle_1, value); }
        }
        private float stepAngle_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.StepAngle_2")]
        public float StepAngle_2
        {
            get { return this.stepAngle_2; }
            set { this.SetProperty(ref this.stepAngle_2, value); }
        }
        private float stepAngle_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.ScrewStep_1")]
        public float ScrewStep_1
        {
            get { return this.screwStep_1; }
            set { this.SetProperty(ref this.screwStep_1, value); }
        }
        private float screwStep_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.ScrewStep_2")]
        public float ScrewStep_2
        {
            get { return this.screwStep_2; }
            set { this.SetProperty(ref this.screwStep_2, value); }
        }
        private float screwStep_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rForceSensorScale")]
        public float ForceSensorScale
        {
            get { return this.forceSensorScale; }
            set { this.SetProperty(ref this.forceSensorScale, value); }
        }
        private float forceSensorScale;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wGC_InitialPos_1")]
        public float GC_InitialPos_1
        {
            get { return this.gC_InitialPos_1; }
            set { this.SetProperty(ref this.gC_InitialPos_1, value); }
        }
        private float gC_InitialPos_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wGC_InitialPos_2")]
        public float GC_InitialPos_2
        {
            get { return this.gC_InitialPos_2; }
            set { this.SetProperty(ref this.gC_InitialPos_2, value); }
        }
        private float gC_InitialPos_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rDistanceBetweenLasers")]
        public float DistanceBetweenLasers
        {
            get { return this.distanceBetweenLasers; }
            set { this.SetProperty(ref this.distanceBetweenLasers, value); }
        }
        private float distanceBetweenLasers;
    }
}
