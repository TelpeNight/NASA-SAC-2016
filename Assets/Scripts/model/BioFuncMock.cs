using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class BioFuncMock : BioFunc
    {
        public override float getRadiationDeathRate(PlanetCondition contitions, Colony colony)
        {
            return 0.9f;
        }

        public override float getTemperatureDeathRate(PlanetCondition contitions, Colony colony)
        {
            return 0.6f;
        }

        public override float getAvarageDeathRate(float radiation, float temperature)
        {
            return (radiation + temperature) / 2;
        }

        public override double getPlanetWater(Colony colony, double requiredWater)
        {
            return requiredWater * 0.3;
        }

        public override double getRequiredCO(Colony colony, int time)
        {
            return colony.getMass() * 0.1 * time;
        }

        public override double getRequiredWater(Colony colony, int time)
        {
            return colony.getMass() * time;
        }

        public override double getRequiredN(Colony colony, int time)
        {
            return colony.getMass() * 0.1 * time;
        }

        public override float getDivRate(Colony colony)
        {
            float ratio = (float)(colony.getMass() / colony.getNum() * 1.5);
            if (ratio >= 2)
            {
                return 2.0f;
            }
            else
            {
                return ratio;
            }
        }

        public override float getUptakeDeathRate(Colony colony, double co, double water, double n, int time)
        {
            return 0.8f;
        }

        public override double getMetabolicWater(Colony colony, int time)
        {
            return 1;
        }

        /*public override float getSmallCellDeathRate(ColonyStats stats)
        {
            if (stats.num > stats.mass * 1.5)
            {
                return 0.7f;
            }
            else
            {
                return 1;
            }
        }*/
    }
}
