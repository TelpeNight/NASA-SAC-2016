using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class Colony
    {
        private ColonyStats _stats;
        private ColonyProduction _production;
        private double _freeWater;
        private ColonyParams _params;

        public Colony(ColonyStats stats, ColonyParams par)
        {
            _stats = stats;
            _params = par;
        }

        private Colony(ColonyStats stats, ColonyParams par, ColonyProduction prod, double water)
        {
            _stats = stats;
            _production = prod;
            _freeWater = water;
            _params = par;
        }

        public Colony die(float avarageDeathRate)
        {
            ColonyStats newStats = _stats;
            newStats.mass *= avarageDeathRate;
            if (newStats.mass < 0.1)
            {
                newStats.mass = 0.001;
            }
            newStats.num *= avarageDeathRate;
            return new Colony(newStats, _params, null, 0);
        }

        public double getFreeWater(float avarageDeathRate)
        {
            return _stats.mass * (1 - avarageDeathRate) * BioFunc.WaterInCell;
        }

        public double getOrganic(float avarageDeathRate)
        {
            return _stats.mass * (1 - avarageDeathRate) * BioFunc.OrganicInCell;
        }

        public double getRequiredCO(int time)
        {
            return BioFunc.funcs.getRequiredCO(this, time);
        }

        public double getRequiredWater(int time)
        {
            return BioFunc.funcs.getRequiredWater(this, time);
        }

        public double getRequiedN(int time)
        {
            return BioFunc.funcs.getRequiredN(this, time);
        }

        public Colony consume(double co, double water, double n, int time)
        {
            var production = new ColonyProduction
            {
                oxygen = co * 32.0 / 44.0,
                surfaceN = n * 17.0 / 28.0
            };
            float uptakeDeathRate = BioFunc.funcs.getUptakeDeathRate(this, co, water, n, time);
            ColonyStats newState = _stats;
            newState.mass *= uptakeDeathRate;
            if (newState.mass < 0.1)
            {
                newState.mass = 0.001;
            }
            newState.num *= uptakeDeathRate;
            return new Colony(newState, _params, production,
                water - BioFunc.funcs.getMetabolicWater(this, time));
        }

        public ColonyProduction getProduction()
        {
            return _production;
        }

        private const long linearMass = 729859829;
        private const long max = 40001877097;
        public Colony massGrow()
        {
            ColonyStats newStats = _stats;
            if (_stats.mass > linearMass)
            {
                newStats.mass += Math.Min(Math.Min(_freeWater, _stats.mass), max / 144000);
            }
            else
            {
                newStats.mass += Math.Min(_freeWater, _stats.mass);
            }
            return new Colony(newStats, _params, null, 0);
        }

        public Colony div()
        {
            ColonyStats newStats = _stats;
            newStats.num *= BioFunc.funcs.getDivRate(this);
            //newStats.num *= BioFunc.funcs.getSmallCellDeathRate(newStats);
            return new Colony(newStats, _params, null, 0);
        }

        public double getMass()
        {
            return _stats.mass;
        }

        public bool isDead()
        {
            return _stats.num < 1 || _stats.mass < 1;
        }

        public double getNum()
        {
            return _stats.num;
        }

        public float getRadiationResistent()
        {
            return _params.radiationResistent;
        }

        public int getOptimalTemperature()
        {
            return _params.optimalTemperature;
        }

        public float getPhotosynthesisPower()
        {
            return _params.photosynthesisPower;
        }

        public double getNFixPower()
        {
            return _params.nFixPower;
        }

        public float getCellSize()
        {
            return _params.cellSize;
        }

    }
}
