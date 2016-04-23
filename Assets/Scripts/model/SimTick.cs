using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class SimTick : SimStep
    {
        private SimStep[] allSteps = {
            new DeathStep(),
            new UptakeStep(),
            new MetaStep(),
            new DivStep(),
            new MutationStep()
        };

        public override StepRes execute(PlanetCondition contitions, Colony colony)
        {
            var currentCond = contitions;
            var currentColony = colony;
            var mutations = new List<Colony>();
            foreach (SimStep step in allSteps)
            {
                var res = step.execute(currentCond, currentColony);
                currentColony = res.colony;
                currentCond = res.conditions;
                if (res.mutations != null)
                {
                    mutations.AddRange(res.mutations);
                }
            }

            return new StepRes
            {
                conditions = currentCond,
                colony = currentColony,
                mutations = mutations
            };
        }
    }
}
