using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    class MutationStep : SimStep
    {
        public override StepRes execute(PlanetCondition conditions, Colony colony)
        {
            //TODO implement
            return new StepRes
            {
                conditions = conditions,
                colony = colony
            };
        }
    }
}
