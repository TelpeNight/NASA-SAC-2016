using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    public class PlanetCondition
    {
        public struct PlanetConditionState
        {
            public double freeWater;
            public double organic;
            public double co;
            public double n;
            public double surfaceN;
            public double oxigen;
            public int temperature;
        }

        private PlanetConditionState _state;
        private int _time;

        public PlanetCondition(PlanetConditionState state, int time)
        {
            _state = state;
            _time = time;
        }

        public PlanetCondition incFreeWater(double water)
        {
            PlanetConditionState newState = _state;
            newState.freeWater += water;
            return new PlanetCondition(newState, _time);
        }

        public PlanetCondition incOrganic(double organic)
        {
            PlanetConditionState newState = _state;
            newState.organic += organic;
            return new PlanetCondition(newState, _time);
        }

        public double getAvailableCO(double requiredCO)
        {
            return Math.Min(requiredCO, _state.co);
        }

        public double getAvailableFreeWater(double requiredWater)
        {
            //TODO
            return Math.Min(requiredWater, _state.freeWater);
        }

        public double getAvailablePlanetWater(Colony colony, double requiredWater)
        {
            return BioFunc.funcs.getPlanetWater(colony, requiredWater);
        }

        public double getAvailableN(double requiredN)
        {
            //TODO
            return Math.Min(requiredN, _state.n);
        }

        public PlanetCondition decCo(double availableCO)
        {
            PlanetConditionState newState = _state;
            newState.co -= availableCO;
            if (newState.co < 0)
            {
                newState.co = 0;
            }
            return new PlanetCondition(newState, _time);
        }

        public PlanetCondition decWater(double availableWater)
        {
            PlanetConditionState newState = _state;
            newState.freeWater -= availableWater;
            if (newState.freeWater < 0)
            {
                newState.freeWater = 0;
            }
            return new PlanetCondition(newState, _time);
        }

        public PlanetCondition decN(double availableN)
        {
            PlanetConditionState newState = _state;
            newState.n -= availableN;
            if (newState.n < 0)
            {
                newState.n = 0;
            }
            return new PlanetCondition(newState, _time);
        }

        public PlanetCondition incOxygen(double oxygen)
        {
            PlanetConditionState newState = _state;
            newState.oxigen += oxygen;
            return new PlanetCondition(newState, _time);
        }

        public PlanetCondition incSurfaceN(double n)
        {
            PlanetConditionState newState = _state;
            newState.surfaceN += n;
            return new PlanetCondition(newState, _time);
        }

        public int getTime()
        {
            return _time;
        }

        public int getTemperature()
        {
            return _state.temperature;
        }

    }
}
