using UnityEngine;
using System.Collections;
using System.Threading;
using model;

public class StartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Thread thread = new Thread(DoSim);
        thread.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DoSim()
    {
        PlanetCondition.PlanetConditionState state = new PlanetCondition.PlanetConditionState
        {
            freeWater = 0,
            organic = 0,
            co = 23830000000000000,
            n = 675000000000000,
            oxigen = 32500000000000,
            temperature = -32
        };

        ColonyStats stats = new ColonyStats
        {
            num = 10,
            mass = 10
        };

        ColonyParams par = new ColonyParams
        {
            radiationResistent = 0.99f,
            optimalTemperature = -20,
            photosynthesisPower = 0.01f,
            nFixPower = 0.001f,
            cellSize = 1
        };

        PlanetCondition contitions = new PlanetCondition(state, 6);
        Colony colony = new Colony(stats, par);

        long iter = 0;
        while (true)
        {
            SimTick tick = new SimTick();
            SimStep.StepRes res = tick.execute(contitions, colony);
            if (double.IsNaN(res.colony.getMass()) || double.IsInfinity(res.colony.getMass())
                || double.IsNaN(res.colony.getNum()) || double.IsInfinity(res.colony.getNum()))
            {
                SimStep.StepRes res2 = tick.execute(contitions, colony);
                break;
            }
            contitions = res.conditions;
            colony = res.colony;
            if (colony.isDead())
            {
                break;
            }
            ++iter;
            if (iter > 100000)
            {
                break;
            }
        }
    }
}
