using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class DeathStep : SimStep
    {
        public override StepRes execute(PlanetCondition conditions, Colony colony)
        {
            float radiationDeathRate = BioFunc.funcs.getRadiationDeathRate(conditions, colony);
            float tempDeathRate = BioFunc.funcs.getTemperatureDeathRate(conditions, colony);
            float avarageDeathRate = BioFunc.funcs.getAvarageDeathRate(radiationDeathRate, tempDeathRate);
            Colony newColony = colony.die(avarageDeathRate);
            PlanetCondition newPlanetCondition = conditions
                .incFreeWater(colony.getFreeWater(avarageDeathRate))
                .incOrganic(colony.getOrganic(avarageDeathRate));
            return new StepRes
            {
                conditions = newPlanetCondition,
                colony = newColony
            };
        }
    }
}
