using BelShina_HMI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.Maths
{
    public class CalcCoef
    {
        public static float Calc(SentDataTab dataTab, object targetClass, string forceName, string wayName, string halfForceName, string halfWayName)
        {
            string sForce = dataTab.DataTable.Rows[dataTab.DataTable.Rows.Count - 1][1].ToString();
            float force = (float)(Convert.ToDouble(sForce));
            string sWay = dataTab.DataTable.Rows[dataTab.DataTable.Rows.Count - 1][0].ToString();
            float way = (float)(Convert.ToDouble(sWay));
            float fHalfForce = force / 2;
            float halfForce = 0f;
            float halfWay = 0f;

            for (int i = 1; i < dataTab.DataTable.Rows.Count; i++)
            {
                string sForce_i = dataTab.DataTable.Rows[i][1].ToString();
                double dForce_i = Convert.ToDouble(sForce_i);
                if ((float)dForce_i > fHalfForce)
                {
                    string sHalfPos = dataTab.DataTable.Rows[i][0].ToString();
                    double dHalfPos = Convert.ToDouble(sHalfPos);
                    string sHalfPosPrevious = dataTab.DataTable.Rows[i - 1][0].ToString();
                    double dHalfPosPrevious = Convert.ToDouble(sHalfPosPrevious);
                    string sFirstPos = dataTab.DataTable.Rows[1][0].ToString();
                    double dFirstPos = Convert.ToDouble(sFirstPos);


                    string sHalfForce = dataTab.DataTable.Rows[i][1].ToString();
                    double dHalfForce = Convert.ToDouble(sHalfForce);
                    string sHalfForcePrevious = dataTab.DataTable.Rows[i - 1][1].ToString();
                    double dHalfForceprevious = Convert.ToDouble(sHalfForcePrevious);
                    halfForce = fHalfForce;//(float)dHalfForce;
                    //halfWay = (float)(dHalfPos);
                    halfWay = (float)Maths.Transform.LinTrafo(fHalfForce, dHalfForce, dHalfForceprevious, dHalfPos, dHalfPosPrevious);
                    halfWay = (float)Math.Round(Convert.ToDecimal(halfWay), 1);
                    way = (float)Math.Round(Convert.ToDecimal(way), 1);
                    break;
                }
            }
            double Kst = (force - halfForce) / (way - halfWay);
            var forceProp = targetClass.GetType().GetProperty(forceName);
            forceProp.SetValue(targetClass, force);
            var wayProp = targetClass.GetType().GetProperty(wayName);
            wayProp.SetValue(targetClass, way);
            var halfForceProp = targetClass.GetType().GetProperty(halfForceName);
            halfForceProp.SetValue(targetClass, halfForce);
            var halfWayProp = targetClass.GetType().GetProperty(halfWayName);
            halfWayProp.SetValue(targetClass, halfWay);


            return (float)Math.Round(Kst, MidpointRounding.AwayFromZero);
        }
    }
}
