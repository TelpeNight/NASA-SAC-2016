using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class MetaStep : SimStep
    {
        public override StepRes execute(PlanetCondition conditions, Colony colony)
        {
            ColonyProduction prod = colony.getProduction();
            PlanetCondition newCond = conditions
                .incOxygen(prod.oxygen)
                .incSurfaceN(prod.surfaceN);
            Colony newCol = colony.massGrow();
            return new StepRes
            {
                conditions = newCond,
                colony = newCol
            };
        }
    }
}
