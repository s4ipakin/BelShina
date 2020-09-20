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
using System.Collections.ObjectModel;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class MainViewModel : SubscriptionBase
    {
        public ICommand ButtonReportsCommand { get; set; }
        
        public MainViewModel()
        {
            
            ButtonReportsCommand = new RelayCommand(o => ReportsButtonClick("ReportsButton"));
            TestTypes = new ObservableCollection<TestType>()
            {
              new TestType(){   Id=1, 
                                Name="Поворот", 
                                Formula="Кугл=(Mα2-Mα1)/(α2-α2)", 
                                HalfForceName = "Mα2",
                                HalfWayName = "α2",
                                ForceName = "Mα1",
                                WayName = "α1",
                                KoefName = "Кугл"
                            }
                    ,new TestType(){
                                        Id=2,
                                        Name="Продольная жесткость",
                                        Formula="Кбок=(Fhб2-Fhб1)/(hб2-hб1)",
                                        HalfForceName = "Fhб2",
                                        HalfWayName = "hб2",
                                        ForceName = "Fhб1",
                                        WayName = "hб1",
                                        KoefName = "Кбок"
                                    }
                    ,new TestType(){
                                        Id=3 , 
                                        Name="Тангенцияльная жесткость",
                                        Formula="Ктанг=(Fhт2-Fhт1)/(hт2-hт1)",
                                        HalfForceName = "Fhт2",
                                        HalfWayName = "hт2",
                                        ForceName = "Fhт1",
                                        WayName = "hт1",
                                        KoefName = "Ктанг"
                                    }
                    ,new TestType(){Id=4 , Name="Тангенциаотная"}
            };

            _testType = TestTypes[0];

        }

        private void ReportsButtonClick(object sender)
        {
            CreatePDF();
            var generateReportsMessage = new GenerateReportsMessage();
            Messenger.Default.Send(generateReportsMessage);
        }
        #region Subscribe

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


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_CycledWay")]
        public float CycledWay
        {
            get 
            { 
                if ((TestTypeProp != null)&&(TestTypeProp.Id == 1))
                {
                    TestTypeProp.WayValue = this.cycledWay;
                }
                return this.cycledWay; 
            }
            set { this.SetProperty(ref this.cycledWay, value); }
        }
        private float cycledWay;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_LinedWay_1")]
        public float LinedWay_1
        {
            get 
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 2))
                {
                    TestTypeProp.WayValue = this.linedWay_1;
                }
                return this.linedWay_1; 
            }
            set { this.SetProperty(ref this.linedWay_1, value); }
        }
        private float linedWay_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_LinedWay_2")]
        public float LinedWay_2
        {
            get
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 3))
                {
                    TestTypeProp.WayValue = this.linedWay_2;
                }
                return this.linedWay_2;
            }
            set { this.SetProperty(ref this.linedWay_2, value); }
        }
        private float linedWay_2;


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


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wProcType")]
        public ushort ProcessType
        {
            get { return this.processType; }
            set { this.SetProperty(ref this.processType, value); }
        }
        private ushort processType;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wST_HalfPos_1")]
        public float HalfPos_1
        {
            get 
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 1))
                {
                    TestTypeProp.HalfWayValue = this.halfPos_1;
                }
                return this.halfPos_1; 
            }
            set { this.SetProperty(ref this.halfPos_1, value); }
        }
        private float halfPos_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wST_HalfPos_2")]
        public float HalfPos_2
        {
            get
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 2))
                {
                    TestTypeProp.HalfWayValue = this.halfPos_2;
                }
                return this.halfPos_2;
            }
            set { this.SetProperty(ref this.halfPos_2, value); }
        }
        private float halfPos_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wST_HalfPos_3")]
        public float HalfPos_3
        {
            get
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 3))
                {
                    TestTypeProp.HalfWayValue = this.halfPos_3;
                }
                return this.halfPos_3;
            }
            set { this.SetProperty(ref this.halfPos_3, value); }
        }
        private float halfPos_3;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wST_HalfForce_1")]
        public float HalfForce_1
        {
            get 
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 1))
                {
                    TestTypeProp.HalfForceValue = this.halfForce_1;
                }
                return this.halfForce_1; 
            }
            set { this.SetProperty(ref this.halfForce_1, value); }
        }
        private float halfForce_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wST_HalfForce_2")]
        public float HalfForce_2
        {
            get
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 2))
                {
                    TestTypeProp.HalfForceValue = this.halfForce_2;
                }
                return this.halfForce_2;
            }
            set { this.SetProperty(ref this.halfForce_2, value); }
        }
        private float halfForce_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.wST_HalfForce_3")]
        public float HalfForce_3
        {
            get
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 3))
                {
                    TestTypeProp.HalfForceValue = this.halfForce_3;
                }
                return this.halfForce_3;
            }
            set { this.SetProperty(ref this.halfForce_3, value); }
        }
        private float halfForce_3;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rST_Koef_1")]
        public float Koef_1
        {
            get 
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 1))
                {
                    TestTypeProp.KoefValue = this.koef_1;
                }
                return this.koef_1; 
            }
            set { this.SetProperty(ref this.koef_1, value); }
        }
        private float koef_1;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rST_Koef_2")]
        public float Koef_2
        {
            get
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 2))
                {
                    TestTypeProp.KoefValue = this.koef_2;
                }
                return this.koef_2;
            }
            set { this.SetProperty(ref this.koef_2, value); }
        }
        private float koef_2;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Process.rST_Koef_3")]
        public float Koef_3
        {
            get
            {
                if ((TestTypeProp != null) && (TestTypeProp.Id == 3))
                {
                    TestTypeProp.KoefValue = this.koef_3;
                }
                return this.koef_3;
            }
            set { this.SetProperty(ref this.koef_3, value); }
        }
        private float koef_3;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_GetForce")]
        public float GetForce
        {
            get { return this.getForce; }
            set { this.SetProperty(ref this.getForce, value); }
        }
        private float getForce;


        #endregion

        private ObservableCollection<TestType> _testTypes;

        public ObservableCollection<TestType> TestTypes
        {
            get { return _testTypes; }
            set { _testTypes = value; }
        }
        private TestType _testType;

        public TestType TestTypeProp
        {
            get { return _testType; }
            set 
            { 
                _testType = value;
                ProcessType = _testType.Id;
            }
        }

        

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

        public string Temperature
        {
            get { return this.temperature; }
            set { this.SetProperty(ref this.temperature, value); }
        }
        private string temperature;

        public string TireNomber
        {
            get { return this.tireNomber; }
            set { this.SetProperty(ref this.tireNomber, value); }
        }
        private string tireNomber;

        public string Department
        {
            get { return this.department; }
            set { this.SetProperty(ref this.department, value); }
        }
        private string department;

        public string TireSize
        {
            get { return this.tireSize; }
            set { this.SetProperty(ref this.tireSize, value); }
        }
        private string tireSize;





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
            dr1[1] = Department;
            dr1[2] = "";
            dataTable.Rows.Add(dr1);
            DataRow dr2 = dataTable.NewRow();
            dr2[0] = "";
            dr2[1] = Fio;
            dr2[2] = "";
            dataTable.Rows.Add(dr2);
            DataRow dr3 = dataTable.NewRow();
            dr3[0] = "";
            dr3[1] = TireType;
            dr2[2] = "";
            dataTable.Rows.Add(dr3);
            DataRow dr4 = dataTable.NewRow();
            dr4[0] = "";
            dr4[1] = TireNomber;
            dr4[2] = "";
            dataTable.Rows.Add(dr4);
            DataRow dr5 = dataTable.NewRow();
            dr5[0] = "";
            dr5[1] = TireSize;
            dr5[2] = "";
            dataTable.Rows.Add(dr5);
            DataRow dr6 = dataTable.NewRow();
            dr6[0] = "";
            dr6[1] = TestTypeProp.Name;
            dr6[2] = "";
            dataTable.Rows.Add(dr6);
            DataRow dr7 = dataTable.NewRow();
            dr7[0] = "Температура";
            dr7[1] = temperature;
            dr7[2] = "°C";
            dataTable.Rows.Add(dr7);
            DataRow dr8 = dataTable.NewRow();
            dr8[0] = "";
            dr8[1] = "Формула";
            dr8[2] = "";
            dataTable.Rows.Add(dr8);
            DataRow dr9 = dataTable.NewRow();
            dr9[0] = "";
            dr9[1] = TestTypeProp.ForceName;
            dr9[2] = "";
            dataTable.Rows.Add(dr9);
            DataRow dr10 = dataTable.NewRow();
            dr10[0] = "";
            dr10[1] = TestTypeProp.HalfForceName;
            dr10[2] = "";
            dataTable.Rows.Add(dr10);
            DataRow dr11 = dataTable.NewRow();
            dr11[0] = "";
            dr11[1] = TestTypeProp.WayName;
            dr11[2] = "";
            dataTable.Rows.Add(dr11);
            DataRow dr12 = dataTable.NewRow();
            dr12[0] = "";
            dr12[1] = TestTypeProp.HalfWayName;
            dr12[2] = "";
            dataTable.Rows.Add(dr12);
            DataRow dr13 = dataTable.NewRow();
            dr13[0] = "";
            dr13[1] = TestTypeProp.KoefName;
            dr13[2] = "";
            dataTable.Rows.Add(dr13);

            DataRow dr14 = dataTable.NewRow();
            dr14[0] = "";
            dr14[1] = TestTypeProp.Formula;
            dr14[2] = "";
            dataTable.Rows.Add(dr14);
            DataRow dr15 = dataTable.NewRow();
            dr15[0] = "";
            dr15[1] = TestTypeProp.ForceValue;
            dr15[2] = "";
            dataTable.Rows.Add(dr15);
            DataRow dr16 = dataTable.NewRow();
            dr16[0] = "";
            dr16[1] = TestTypeProp.HalfForceValue;
            dr16[2] = "";
            dataTable.Rows.Add(dr16);
            DataRow dr17 = dataTable.NewRow();
            dr17[0] = "";
            dr17[1] = TestTypeProp.WayValue;
            dr17[2] = "";
            dataTable.Rows.Add(dr17);
            DataRow dr18 = dataTable.NewRow();
            dr18[0] = "";
            dr18[1] = TestTypeProp.HalfWayValue;
            dr18[2] = "";
            dataTable.Rows.Add(dr18);
            DataRow dr19 = dataTable.NewRow();
            dr19[0] = "";
            dr19[1] = TestTypeProp.KoefValue;
            dr19[2] = "";
            dataTable.Rows.Add(dr19);

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
