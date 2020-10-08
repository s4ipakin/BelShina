using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.ViewModels
{
    public class ValuePair
    {
        public ValuePair(float stepDistance, float laserDistance)
        {
            this._stepDistance = stepDistance;
            this._laserDistance = laserDistance;
        }
        private float _stepDistance;
        public float StepDistance
        {
            get { return _stepDistance; }
            set { _stepDistance = value; }
        }

        private float _laserDistance;
        public float LaserDistance
        {
            get { return _laserDistance; }
            set { _laserDistance = value; }
        }
    }
}
