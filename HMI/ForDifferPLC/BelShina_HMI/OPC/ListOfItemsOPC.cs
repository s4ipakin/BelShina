﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.OPC
{
    class ListOfItemsOPC
    {
        List<string> itemsList = new List<string>()
        {
            "Application.HMI_Stepper.rFS_GetForce",
            "Application.HMI_Stepper.rFS_CycledWay",
            "Application.HMI_Stepper.rFS_LinedWay_1",
            "Application.HMI_Stepper.rFS_LinedWay_2",
            "Application.HMI_Stepper.rLS_RealPos_1",
            "Application.HMI_Stepper.rLS_RealPos_2"
        };

        public List<string> GetOPCitems()
        {
            return itemsList;
        }
    }
}
