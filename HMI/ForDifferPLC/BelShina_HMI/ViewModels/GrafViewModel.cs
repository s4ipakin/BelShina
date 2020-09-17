using BelShina_HMI.Chart;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;

namespace BelShina_HMI.ViewModels
{
    [Subscription(endpointUrl: "opc.tcp://192.168.1.17:4840", publishingInterval: 500, keepAliveCount: 20)]
    public class GrafViewModel : SubscriptionBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels
        {
            get { return this._labels; }
            set { this.SetProperty(ref this._labels, value); }
        }
        
        protected string[] _labels;
        protected GrafSeries[] arSeries;
        public Func<double, string> YFormatter { get; set; }
        protected string firstValueName;
        protected string secondValueName;

        SeriesCollectionHandler seriesCollectionHandler = new SeriesCollectionHandler();
        protected DataTable dataTable = new DataTable();

        /// ///////////////////////
    
        public GrafViewModel(GrafSet grafSet)
        {
            arSeries = grafSet.GetSettings();
            SeriesCollection = new SeriesCollection();
            Labels = new[] { System.DateTime.Now.ToString() };
            YFormatter = value => value.ToString() + grafSet.unit;
            dataTable.Columns.Add("FirstValue");
            dataTable.Columns.Add("SecondValue");
            for (int i = 0; i < arSeries.Length; i++)
            {
                SeriesCollection.Add(arSeries[i].LineSeries);
            }
        }

        /// /////////////////////// ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_ActualPos

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.wFS_ActualPos")]
        public long ActualPosition
        {
            get { return this.actualPosition; }
            set { this.SetProperty(ref this.actualPosition, value); }
        }

        private long actualPosition;
        
        /// ///////////////////////////////////////////////
        

        [MonitoredItem(nodeId: "ns=4;s=|var|WAGO 750-8202 PFC200 2ETH RS Tele T ECO.Application.HMI_Stepper.rFS_GetForce")]
        public float GetForse
        {
            get 
            {
                DataRow dr = dataTable.NewRow();
                dr[0] = ActualPosition.ToString();
                dr[1] = this.getForse.ToString();
                dataTable.Rows.Add(dr);
                Labels = seriesCollectionHandler.SetValues(SeriesCollection[0].Values, dataTable, 0, 1);
                return this.getForse;
                
            }
            set { this.SetProperty(ref this.getForse, value); }
        }

        private float getForse;
        /// ///////////////////////////////////////////////
    }
}
