using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public abstract class BioFunc
    {
        public static BioFunc funcs = new BioFuncImpl();
        public static double WaterInCell = 0.9;
        public static double OrganicInCell = 0.001;

        public abstract float getRadiationDeathRate(PlanetCondition contitions, Colony colony);
        public abstract float getTemperatureDeathRate(PlanetCondition contitions, Colony colony);
        public abstract float getAvarageDeathRate(float radiation, float temperature);

        public abstract double getPlanetWater(Colony colony, double requiredWater);

        public abstract double getRequiredCO(Colony colony, int time);
        public abstract double getRequiredWater(Colony colony, int time);
        public abstract double getRequiredN(Colony colony, int time);
        public abstract double getMetabolicWater(Colony colony, int time);

        public abstract float getDivRate(Colony colony);
        //public abstract float getSmallCellDeathRate(ColonyStats stats);
        public abstract float getUptakeDeathRate(Colony colony, double co, double water, double n, int time);
    }
}
