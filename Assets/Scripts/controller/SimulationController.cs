using System;
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
        }

        public void start(int ticks)
        {
            if (_simTread != null)
            {
                return;
            }

            _simTread = new Thread(() => {
                model.PlanetCondition conditions = _conditions;
                model.Colony colony = _currentColony;

                if (colony.isDead())
                {
                    return;
                }

                for (int i = 0; i < ticks; ++i)
                {
                    SimTick tick = new SimTick();
                    SimStep.StepRes res = tick.execute(conditions, colony);
                    if (res.colony.isDead())
                    {
                        break;
                    }
                    conditions = res.conditions;
                    colony = res.colony;
                }

                _conditions = conditions;
                _currentColony = colony;
                //TODO mutate
            });
            _simTread.Start();
        }

        public bool ticksFinished()
        {
            if (_simTread.IsAlive)
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
