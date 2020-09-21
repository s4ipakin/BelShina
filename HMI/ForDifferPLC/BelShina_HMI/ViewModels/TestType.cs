using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.ViewModels
{
    public class TestType
    {
        private ushort _id;
        public ushort Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _halfForceName;
        public string HalfForceName
        {
            get { return _halfForceName; }
            set { _halfForceName = value; }
        }

        private string _halfWayName;
        public string HalfWayName
        {
            get { return _halfWayName; }
            set { _halfWayName = value; }
        }

        private float _halfForceValue;
        public float HalfForceValue
        {
            get { return _halfForceValue; }
            set { _halfForceValue = value; }
        }

        private float _halfWayValue;
        public float HalfWayValue
        {
            get { return _halfWayValue; }
            set { _halfWayValue = value; }
        }

        private string _formula;
        public string Formula
        {
            get { return _formula; }
            set { _formula = value; }
        }

        private float _forceValue;
        public float ForceValue
        {
            get { return _forceValue; }
            set { _forceValue = value; }
        }

        private string _forceName;
        public string ForceName
        {
            get { return _forceName; }
            set { _forceName = value; }
        }

        private float _wayValue;
        public float WayValue
        {
            get { return _wayValue; }
            set { _wayValue = value; }
        }

        private string _wayName;
        public string WayName
        {
            get { return _wayName; }
            set { _wayName = value; }
        }

        private float _koefValue;
        public float KoefValue
        {
            get { return _koefValue; }
            set { _koefValue = value; }
        }

        private string _koefName;
        public string KoefName
        {
            get { return _koefName; }
            set { _koefName = value; }
        }

        private string _wayDiscr;
        public string WayDiscr
        {
            get { return _wayDiscr; }
            set { _wayDiscr = value; }
        }

        private string _halfwayDiscr;
        public string HalfWayDiscr
        {
            get { return _halfwayDiscr; }
            set { _halfwayDiscr = value; }
        }

        private string _forceDiscr;
        public string ForceDiscr
        {
            get { return _forceDiscr; }
            set { _forceDiscr = value; }
        }

        private string _halfforceDiscr;
        public string HalfForceDiscr
        {
            get { return _halfforceDiscr; }
            set { _halfforceDiscr = value; }
        }

        private string _koefDiscr;
        public string KoefForceDiscr
        {
            get { return _koefDiscr; }
            set { _koefDiscr = value; }
        }

        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
    }
}
