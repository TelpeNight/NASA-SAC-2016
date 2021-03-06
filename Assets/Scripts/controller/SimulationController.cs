﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using model;

namespace controller
{
    class SimulationController
    {
        private model.Colony _currentColony;
        private model.PlanetCondition _conditions;
        private Thread _simTread;
        private int totalTicks = 0;
        private int defaultTemp;

        public SimulationController()
        {
            model.PlanetCondition.PlanetConditionState state = new model.PlanetCondition.PlanetConditionState
            {
                freeWater = 0,
                organic = 0,
                co = 23830000000000000,
                n = 675000000000000,
                oxigen = 32500000000000,
                //TODO temperature
                temperature = -40
            };

            model.ColonyStats stats = new model.ColonyStats
            {
                num = 10,
                mass = 10
            };

            model.ColonyParams par = ParamsController.colonyParams;
                
            model.PlanetCondition contitions = new model.PlanetCondition(state, ParamsController.growthSpeed);
            model.Colony colony = new model.Colony(stats, par);

            _currentColony = colony;
            _conditions = contitions;
            defaultTemp = _conditions.getTemperature();
        }

        public void start(int ticks)
        {
            if (!ticksFinished())
            {
                return;
            }

//             if (totalTicks % 8 == 0)
//             {
//                 _conditions = _conditions.withTempearture(-60);
//             }

            _simTread = new Thread(() => {
                model.PlanetCondition conditions = _conditions;
                model.Colony colony = _currentColony;

                for (int i = 0; i < ticks; ++i)
                {
                    if (colony.isDead())
                    {
                        break;
                    }
                    SimTick tick = new SimTick();
                    SimStep.StepRes res = tick.execute(conditions, colony);
                    conditions = res.conditions;
                    colony = res.colony;
                    ++totalTicks;
                }

                _conditions = conditions.withTempearture(defaultTemp);
                _currentColony = colony;
                //TODO mutate
            });
            _simTread.Start();
        }

        public bool ticksFinished()
        {
            if (_simTread != null && _simTread.IsAlive)
            {
                return false;
            }
            _simTread = null;
            return true;
        }

        public PlanetCondition getConditions()
        {
            if (!ticksFinished())
            {
                return null;
            }
            return _conditions;
        }

        public Colony getColony()
        {
            if (!ticksFinished())
            {
                return null;
            }
            return _currentColony;
        }
    }
}
