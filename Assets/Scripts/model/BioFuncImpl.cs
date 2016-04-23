using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    class BioFuncImpl : BioFunc
    {
        public const int MinPeriod = 6;

        public override float getRadiationDeathRate(PlanetCondition conditions, Colony colony)
        {
            return (float)Math.Pow(colony.getRadiationResistent(), conditions.getTime() / 6.0);
        }

        public override float getTemperatureDeathRate(PlanetCondition conditions, Colony colony)
        {
            if (colony.getOptimalTemperature() <= conditions.getTemperature())
            {
                return (float)Math.Pow(0.99, conditions.getTime() / 6.0);
            }
            else
            {
                int delta = colony.getOptimalTemperature() - conditions.getTemperature();
                float deathRate = Math.Max(1 - delta * 2 / 100f, 0);
                return (float)Math.Pow(deathRate, conditions.getTime() / 6.0);
            }
        }

        public override float getAvarageDeathRate(float radiation, float temperature)
        {
            return Math.Min(radiation, temperature);
        }

        public override double getPlanetWater(Colony colony, double requiredWater)
        {
            //TODO large colonies can't find water
            if (colony.getMass() > 5000)
            {
                return requiredWater * 0.5;
            }
            else
            {
                return requiredWater;
            }
        }

        public override double getRequiredCO(Colony colony, int time)
        {
            return colony.getPhotosynthesisPower()*colony.getMass() * time;
        }

        public override double getRequiredWater(Colony colony, int time)
        {
            double metabolism = getMetabolicWater(colony, time);
            return metabolism + colony.getMass();
        }

        public override double getRequiredN(Colony colony, int time)
        {
            //TODO
            return colony.getNFixPower() * colony.getMass() * time;
        }

        public override float getDivRate(Colony colony)
        {
            if (colony.getNum() == 0)
            {
                return 0;
            }
            return (float)(colony.getMass() / colony.getNum() / colony.getCellSize());
        }

        public override float getUptakeDeathRate(Colony colony, double co, double water, double n, int time)
        {
            //TODO
            float deathRate = 1;
            if (water < getMetabolicWater(colony, time))
            {
                deathRate = 0.9f;
            }
            if (co < getRequiredCO(colony, time))
            {
                deathRate = 0.7f;
            }
            if (n < getRequiredN(colony, time))
            {
                deathRate = 0.7f;
            }
            return deathRate;
        }

        public override double getMetabolicWater(Colony colony, int time)
        {
            return getRequiredCO(colony, time) / 44 * 18;
        }
    }
}
