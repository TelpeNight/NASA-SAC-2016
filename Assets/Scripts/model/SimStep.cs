using System.Collections.Generic;

namespace model
{
    public abstract class SimStep
    {
        public struct StepRes
        {
            public PlanetCondition conditions;
            public Colony colony;
            public List<Colony> mutations;
        }

        public abstract StepRes execute(PlanetCondition conditions, Colony colony);
    }
}