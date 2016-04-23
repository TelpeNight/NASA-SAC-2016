using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class UptakeStep : SimStep
    {
        public override StepRes execute(PlanetCondition conditions, Colony colony)
        {
            double requiredCO = colony.getRequiredCO(conditions.getTime());
            double requiredWater = colony.getRequiredWater(conditions.getTime());
            double requiredN = colony.getRequiedN(conditions.getTime());

            double availableCO = conditions.getAvailableCO(requiredCO);
            double availableFreeWater = conditions.getAvailableFreeWater(requiredWater);
            double availablePlanetWater = conditions.getAvailablePlanetWater(colony, requiredWater);
            double availableN = conditions.getAvailableN(requiredN);

            double water = availableFreeWater;
            if (water < requiredWater)
            {
                water += Math.Min(availablePlanetWater, requiredWater - water);
            }

            Colony newColony = colony.consume(co: availableCO, water: water, n: availableN, time: conditions.getTime());
            PlanetCondition newCondition = conditions
                .decCo(availableCO)
                .decWater(water)
                .decN(availableN);
            return new StepRes
            {
                conditions = newCondition,
                colony = newColony
            };
        }
    }
}
