using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Workstation.ServiceModel.Ua;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class AlmViewModel : SubscriptionBase
    {
        private DataTable tblCurrent;
       
        private DataTable tbl;
        public ICommand ButtonShowHystoryCommand { get; set; }
        public ICommand ButtonShowCurrentCommand { get; set; }
        public ICommand ButtonAcknowlage { get; set; }
        public ICommand ButtonClear { get; set; }

        public AlmViewModel()
        {
            tbl = new DataTable("Logs");
            
            tblCurrent = new DataTable("LogsCur");
            ButtonShowHystoryCommand = new RelayCommand(o => HystoryBtnClck("HystoryBtn"));
            ButtonShowCurrentCommand = new RelayCommand(o => CurrentBtnClck("CurrentBtn"));
            ButtonAcknowlage = new RelayCommand(o => Acknowlage("ShowBtn"));
            ButtonClear = new RelayCommand(o => ClearHystory("ClearBtn"));

            tbl.Columns.Add("Date", typeof(string));
            tbl.Columns.Add("Message", typeof(string));
            tbl.Columns.Add("EntryType", typeof(EventLogEntryType));
            tblCurrent = tbl.Copy();
        }

        private void ClearHystory(string v)
        {
            using (EventLog eventLog = new EventLog("MyNewLog"))
            {
                eventLog.Source = "MySourceOPC";
                eventLog.Clear();
                tbl.Clear();
                tblCurrent.Clear();
            }
        }

        private void Acknowlage(string v)
        {
            if((tbl.Rows.Count >= SelectedInde1) &&(SelectedInde1 != -1))
            {
                if (((EventLogEntryType)tbl.Rows[SelectedInde1][2] == EventLogEntryType.Error) && (!isHystory))
                {
                    //MessageBox.Show(SelectedInde1.ToString());
                    tblCurrent.Rows.RemoveAt(SelectedInde1);
                    tbl.Rows.RemoveAt(SelectedInde1);
                }
            }
                
        }

        private void HystoryBtnClck(object sender)
        {
            isHystory = true;
            tbl.Clear();
            using (EventLog eventLog = new EventLog("MyNewLog"))
            {
                EventLog myLog = new EventLog();
                myLog.Source = "MySourceOPC";
                foreach (EventLogEntry entry in myLog.Entries)
                {
                    tbl.Rows.Add(entry.TimeWritten.ToString(), entry.Message, entry.EntryType);
                }
            }
            
        }

        private bool isHystory = false;

        private void CurrentBtnClck(object sender)
        {
            isHystory = false;
            tbl.Clear();
            foreach (DataRow dataRow in tblCurrent.Rows)
            {
                tbl.Rows.Add(dataRow[0], dataRow[1], dataRow[2]);
            }
        }

        private void EnterEvent(string message, EventLogEntryType type)
        {
            
            tbl.Clear();          
            using (EventLog eventLog = new EventLog("MyNewLog"))
            {
                eventLog.Source = "MySourceOPC";
                eventLog.WriteEntry(message, type, 101, 1);
            }
            tblCurrent.Rows.Add(DateTime.Now.ToString(), message, type);
            if (!isHystory)
            {
                foreach (DataRow dataRow in tblCurrent.Rows)
                {
                    tbl.Rows.Add(dataRow[0], dataRow[1], dataRow[2]);
                }
            }
            else
            {
                EventLog myLog = new EventLog();
                myLog.Source = "MySourceOPC";
                foreach (EventLogEntry entry in myLog.Entries)
                {
                    tbl.Rows.Add(entry.TimeWritten, entry.Message, entry.EntryType);
                }
            }
            
        }

        public int SelectedInde1
        {
            get { return selectedIndex; }
            set 
            { 
                selectedIndex = value;
                
            }
        }
        public int selectedIndex;

        public DataTable EventTable
        {
            get { return tbl; }
            set { tbl = value; }
        }

        public DataRow SelectedRow
        {
            get { return iSelectedRow; }
            set
            {
                
                iSelectedRow = value;
            }
        }
        private DataRow iSelectedRow = null;

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_State")]
        public ushort FS_State
        {
            get 
            { 
                if (this.fS_State == 2)
                {
                    EnterEvent("Усилие через метод", EventLogEntryType.Information);
                }
                return this.fS_State; 
            }
            set { this.SetProperty(ref this.fS_State, value); }
        }
        private ushort fS_State;


        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Alm.wForceStepAlm")]
        public ushort ForceStepAlm
        {
            get 
            { 
                if (this.forceStepAlm > 0)
                {
                    EnterEvent("Некая авария", EventLogEntryType.Error);
                }
                return this.forceStepAlm; 
            }
            set { this.SetProperty(ref this.forceStepAlm, value); }
        }
        private ushort forceStepAlm;
    }
}
