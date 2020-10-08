using BelShina_HMI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BelShina_HMI.Chart
{
    public class ConturApprox
    {
        int previousStepper;
        float previousLaser;

        public List<ValuePair> GetPoints(float stepper, float laser)
        {
            List<ValuePair> list = new List<ValuePair>(); 
            float _stepper = 0f;
            float _laser = 0f;
            if ((previousStepper == 0f) && (previousLaser == 0))
            {
                _laser = laser;
                _stepper = 0f;
                previousStepper = 0;
                previousLaser = _laser;
                list.Add(new ValuePair(_stepper, _laser));
            }
            else
            {
                int roundStepper = (int)stepper;
                int count = roundStepper - previousStepper;
                //float previousApproxLaser = 0f;
                for (int i = 0; i < count; i++)
                {
                    _stepper = previousStepper + 1;
                    _laser = previousLaser - (((i + 1)/ (stepper - previousStepper)) * (previousLaser - laser));
                    //MessageBox.Show(_laser.ToString());
                    list.Add(new ValuePair(_stepper, _laser));
                }
                previousLaser = _laser;
                previousStepper = roundStepper;

            }
            return list;
        }

        public bool Check(float valueWhole, float value)
        {
            if ((previousLaser == 0f) && (previousStepper == 0))
            {
                return true;
            }
            else
            {
                int roundKey = (int)valueWhole;
                int count = roundKey - previousStepper;
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            previousStepper = 0;
            previousLaser = 0;
        }
    }
}
