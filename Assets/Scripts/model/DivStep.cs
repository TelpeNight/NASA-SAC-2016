using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class DivStep : SimStep
    {
        public override StepRes execute(PlanetCondition conditions, Colony colony)
        {
            return new StepRes
            {
                conditions = conditions,
                colony = colony.div()
            };
        }
    }
}
