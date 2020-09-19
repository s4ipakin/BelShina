using BelShina_HMI.Reports;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class MainViewModel : SubscriptionBase
    {
        public ICommand ButtonReportsCommand { get; set; }
        public MainViewModel()
        {
            ButtonReportsCommand = new RelayCommand(o => ReportsButtonClick("ReportsButton"));
        }

        private void ReportsButtonClick(object sender)
        {
            CreatePDF();
            var generateReportsMessage = new GenerateReportsMessage();
            Messenger.Default.Send(generateReportsMessage);
        }

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_SetForce")]
        public float SetForce
        {
            get { return this.setForce; }
            set { this.SetProperty(ref this.setForce, value); }
        }

        private float setForce;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.xFS_Release")]
        public bool Release
        {
            get { return this.release; }
            set { this.SetProperty(ref this.release, value); }
        }

        private bool release;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.xFS_Start")]
        public bool Start
        {
            get { return this.start; }
            set { this.SetProperty(ref this.start, value); }
        }

        private bool start;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_ActualPos")]
        public long ActualPosition
        {
            get { return this.actualPosition; }
            set { this.SetProperty(ref this.actualPosition, value); }
        }

        private long actualPosition;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.xFS_JogLeft")]
        public bool JogLeft
        {
            get { return this.jogLeft; }
            set { this.SetProperty(ref this.jogLeft, value); }
        }

        private bool jogLeft;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.xFS_JogRight")]
        public bool JogRight
        {
            get { return this.jogRight; }
            set { this.SetProperty(ref this.jogRight, value); }
        }

        private bool jogRight;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_State")]
        public ushort FS_State
        {
            get { return this.fS_State; }
            set { this.SetProperty(ref this.fS_State, value); }
        }

        private ushort fS_State;

        //[MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.xProcFinished")]
        //public bool ProcFinished
        //{
        //    get
        //    {
        //        if (this.procFinished)
        //        {
        //            CreatePDF();
        //        }
        //        return this.procFinished;
        //    }
        //    set { this.SetProperty(ref this.procFinished, value); }
        //}

        

        protected bool procFinished;


        ////////////////////////////////////////
        ///

        public string Fio
        {
            get { return this.fIo; }
            set { this.SetProperty(ref this.fIo, value); }
        }

        private string fIo;

        public string TireType
        {
            get { return this.tireType; }
            set { this.SetProperty(ref this.tireType, value); }
        }

        private string tireType;

        public string TestType
        {
            get { return this.testTipe; }
            set { this.SetProperty(ref this.testTipe, value); }
        }

        private string testTipe;

        ///////////////////////
        ///

        private void CreatePDF()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Parametr");
            dataTable.Columns.Add("Value");
            dataTable.Columns.Add("Unit");
            DataRow dr1 = dataTable.NewRow();
            dr1[0] = "";
            dr1[1] = fIo;
            dr1[2] = "";
            dataTable.Rows.Add(dr1);
            DataRow dr2 = dataTable.NewRow();
            dr2[0] = "";
            dr2[1] = tireType;
            dr2[2] = "";
            dataTable.Rows.Add(dr2);
            DataRow dr3 = dataTable.NewRow();
            dr3[0] = "";
            dr3[1] = testTipe;
            dr3[2] = "";
            dataTable.Rows.Add(dr3);
            DataRow dr4 = dataTable.NewRow();
            dr4[0] = "Заданная сила";
            dr4[1] = setForce;
            dr4[2] = "Н";
            dataTable.Rows.Add(dr4);
            DataRow dr5 = dataTable.NewRow();
            dr5[0] = "Смещение";
            dr5[1] = actualPosition;
            dr5[2] = "мм";
            dataTable.Rows.Add(dr5);
            //try
            //{
                PDF_Tab pDF_Tab = new PDF_Tab("/Projects/PDF/Invoice/invoice.xml", dataTable);
                var document = pDF_Tab.CreateDocument();
                document.UseCmykColor = true;
                var pdfRenderer = new PdfDocumentRenderer(true);
                pdfRenderer.Document = document;
                pdfRenderer.RenderDocument();
                var filename = "Invoice.pdf";
                pdfRenderer.Save(filename);
                Process.Start(filename);
            //}
            //catch (Exception ex) { }
            

        }

    }
}
