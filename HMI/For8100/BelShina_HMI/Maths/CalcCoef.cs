using BelShina_HMI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.Maths
{
    public class CalcCoef
    {
        public static float Calc(DataTable dataTab, object targetClass, string forceName, string wayName, string halfForceName, string halfWayName, bool setMax = false, string max = "0", string min = "0")
        {
            string sForce;
            string sWay = "0";
            float force;
            if (setMax)
            {
                sForce = max;
                force = (float)(Convert.ToDouble(sForce));
                for (int i = 1; i < dataTab.Rows.Count; i++)
                {
                    string sForce_i = dataTab.Rows[i][1].ToString();
                    double dForce_i = Convert.ToDouble(sForce_i);
                    if ((float)dForce_i > force)
                    {
                        
                        string sForce_i_Previous = dataTab.Rows[i - 1][1].ToString();
                        double dForce_i_Previous = Convert.ToDouble(sForce_i_Previous);
                        string sPose_i = dataTab.Rows[i][0].ToString();
                        double dPose_i = Convert.ToDouble(sPose_i);
                        string sPose_i_Previous = dataTab.Rows[i - 1][0].ToString();
                        double dPose_i_Previous = Convert.ToDouble(sPose_i_Previous);
                        double dWay = Maths.Transform.LinTrafo(force, dForce_i, dForce_i_Previous, dPose_i, dPose_i_Previous);
                        //dWay = Math.Round(dWay, 1);
                        sWay = dWay.ToString();
                        break;
                    }
                }
            }
            else
            {
                sForce = dataTab.Rows[dataTab.Rows.Count - 1][1].ToString();
                sWay = dataTab.Rows[dataTab.Rows.Count - 1][0].ToString();
                force = (float)(Convert.ToDouble(sForce));
            }
            
            
            float way = (float)(Convert.ToDouble(sWay));
            float fHalfForce = force / 2;
            float halfForce = 0f;
            float halfWay = 0f;

            for (int i = 1; i < dataTab.Rows.Count; i++)
            {
                string sForce_i = dataTab.Rows[i][1].ToString();
                double dForce_i = Convert.ToDouble(sForce_i);
                if ((float)dForce_i > fHalfForce)
                {
                    string sHalfPos = dataTab.Rows[i][0].ToString();
                    double dHalfPos = Convert.ToDouble(sHalfPos);
                    string sHalfPosPrevious = dataTab.Rows[i - 1][0].ToString();
                    double dHalfPosPrevious = Convert.ToDouble(sHalfPosPrevious);
                    string sFirstPos = dataTab.Rows[1][0].ToString();
                    double dFirstPos = Convert.ToDouble(sFirstPos);


                    string sHalfForce = dataTab.Rows[i][1].ToString();
                    double dHalfForce = Convert.ToDouble(sHalfForce);
                    string sHalfForcePrevious = dataTab.Rows[i - 1][1].ToString();
                    double dHalfForceprevious = Convert.ToDouble(sHalfForcePrevious);
                    halfForce = fHalfForce;//(float)dHalfForce;
                    //halfWay = (float)(dHalfPos);
                    halfWay = (float)Maths.Transform.LinTrafo(fHalfForce, dHalfForce, dHalfForceprevious, dHalfPos, dHalfPosPrevious);
                    
                    //way = (float)Math.Round(Convert.ToDecimal(way), 1);
                    break;
                }
            }
            double Kst = (force - halfForce) / (way - halfWay);
            //halfWay = (float)Math.Round(Convert.ToDecimal(halfWay), 1);
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
