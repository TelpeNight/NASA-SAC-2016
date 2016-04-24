using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    class BioFuncImpl : BioFunc
    {
        public const int MinPeriod = 6;

        private float timeTick(int time)
        {
            return (time) / MinPeriod;
        }

        public override float getRadiationDeathRate(PlanetCondition conditions, Colony colony)
        {
            double a = colony.getRadiationResistent();
            double b = timeTick(conditions.getTime());
            float res = (float)Math.Pow(a, b);
            if (res < 0.001)
            {
                float res2 = (float)Math.Pow(a, b);
                res = res2;
            }
            return res;
        }

        public override float getTemperatureDeathRate(PlanetCondition conditions, Colony colony)
        {
            double a = 0.999;
            double b = timeTick(conditions.getTime());
            if (colony.getOptimalTemperature() > conditions.getTemperature())
            {
                int delta = colony.getOptimalTemperature() - conditions.getTemperature();
                a = Math.Max(1 - delta * 0.1f / 100f, 0);
            }
            float res = (float)Math.Pow(a, b);
            if (res < 0.001)
            {
                float res2 = (float)Math.Pow(a, b);
                res = res2;
            }
            return res;
        }

        public override float getAvarageDeathRate(float radiation, float temperature)
        {
            float sum = radiation * temperature;
            float delta = Math.Min(radiation, temperature) - sum;
            return sum + delta/2;
        }

        private static float getPlanetCoe(double mass)
        {
            double rate = 27200000000 / mass;
            if (rate > 1)
            {
                return 1;
            }
            else
            {
                return (float)rate;
            }
        }

        public override double getPlanetWater(PlanetCondition conditions, Colony colony, double requiredWater)
        {
            return requiredWater * getPlanetCoe(colony.getMass());
        }

        public override double getPlanetCO(PlanetCondition contitions, Colony colony, double requiredCO)
        {
            float coe = 0.3f + getPlanetCoe(colony.getMass());
            if (coe > 1)
            {
                coe = 1;
            }
            return requiredCO * coe;
        }

        public override double getPlanetN(PlanetCondition contitions, Colony colony, double requiredN)
        {
            float coe = 0.3f + getPlanetCoe(colony.getMass());
            if (coe > 1)
            {
                coe = 1;
            }
            return requiredN * coe;
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

        private static float AVG_RATE = 0.515f;
        public override float getUptakeDeathRate(Colony colony, double co, double water, double n, int time)
        {
            float rate = AVG_RATE;
            if (co < getRequiredCO(colony, time))
            {
                rate *= (float)(co / getRequiredCO(colony, time));
            }
            if (n < getRequiredN(colony, time))
            {
                rate *= (float)(n / getRequiredN(colony, time));
            }
            return rate;
        }

        public override double getMetabolicWater(Colony colony, int time)
        {
            return getRequiredCO(colony, time) / 44 * 18;
        }
    }
}
