using System;
using System.Collections.Generic;
using System.Text;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Crossroads
{
    public class PriorityToTheRightCrossroads : Base.Crossroads
    {
        public override CrossroadsType CrossroadsType => CrossroadsType.RightPriority;
    }
}
