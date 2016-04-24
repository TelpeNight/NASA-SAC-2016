using UnityEngine;
using System.Collections.Generic;

public class SimulationUIController : MonoBehaviour
{
    public List<ColonyUI> colonies = new List<ColonyUI>();
    public Canvas screenCanvas;

    private UnityEngine.UI.Text _temperatureText;
    private UnityEngine.UI.Text _pressureText;
    private UnityEngine.UI.Text _radiationText;
    private UnityEngine.UI.Text _humidityText;
    private UnityEngine.UI.Text _CO2Text;
    private UnityEngine.UI.Text _O2Text;
    private UnityEngine.UI.Text _N2atmosphereText;
    private UnityEngine.UI.Text _N2groundText;
    private UnityEngine.UI.Text _organicText;

    [SerializeField]
    private long _simualationPeriod = 0;
    private int _simualtionStep = 1;
    private UnityEngine.UI.Text _timeText;

    private controller.SimulationController _simulationController = null;
    private Timer _timer = null;
    private bool _waitForSimulation = false;

    private const int LARGE_UPDATE_PERIOD = 5;
    private int _currentTick = 0;


    // Use this for initialization
    void Start()
    {
        _temperatureText = screenCanvas.transform.FindChild("Temperature").GetComponent<UnityEngine.UI.Text>();
        _pressureText = screenCanvas.transform.FindChild("Pressure").GetComponent<UnityEngine.UI.Text>();
        _radiationText = screenCanvas.transform.FindChild("Radiation").GetComponent<UnityEngine.UI.Text>();
        _CO2Text = screenCanvas.transform.FindChild("CO2").GetComponent<UnityEngine.UI.Text>();
        _O2Text = screenCanvas.transform.FindChild("O2").GetComponent<UnityEngine.UI.Text>();
        _N2atmosphereText = screenCanvas.transform.FindChild("N").GetComponent<UnityEngine.UI.Text>();
        _N2groundText = screenCanvas.transform.FindChild("Nground").GetComponent<UnityEngine.UI.Text>();
        _organicText = screenCanvas.transform.FindChild("Organic").GetComponent<UnityEngine.UI.Text>();
        _timeText = screenCanvas.transform.FindChild("Timer").GetComponent<UnityEngine.UI.Text>();

        StartInitialSimulation();
        UpdatePeriodUI();
        //UpdateUI();
    }

    void StartInitialSimulation()
    {
        _simulationController = new controller.SimulationController();

        int ticks = (30 * _simualtionStep) * 12 / controller.ParamsController.growthSpeed / LARGE_UPDATE_PERIOD;
        _simulationController.start(ticks);
        _currentTick++;

        StartTimer();
    }

    void StartTimer()
    {
        _timer = gameObject.AddComponent<Timer>();
        _timer.Init(0.2f);
        _timer.onTimeout += OnTimerFinished;
        _timer.Run();
    }

    void OnTimerFinished()
    {
        _timer.onTimeout -= OnTimerFinished;
        Destroy(_timer);
        _timer = null;

        if (_simulationController.ticksFinished())
        {
            FinishSimulationTick();
        }
        else
        {
            _waitForSimulation = true;
        }
    }

    void FinishSimulationTick()
    {
        if (_currentTick == LARGE_UPDATE_PERIOD)
        {
            IncreaseSimulationPeriod();
            _currentTick = 0;
        }

        UpdateUI();
        int ticks = (30 * _simualtionStep) * 12 / controller.ParamsController.growthSpeed / LARGE_UPDATE_PERIOD;
        _simulationController.start(ticks);
        _currentTick++;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_waitForSimulation)
        {
            if (_simulationController.ticksFinished())
            {
                _waitForSimulation = false;
                FinishSimulationTick();
            }
        }
    }

    void UpdateUI()
    {
        UpdatePlanetInfo();
        UpdateColonies();
    }

    public void IncreaseSimulationPeriod()
    {
        _simualationPeriod += _simualtionStep;
        UpdatePeriodUI();
    }

    public void OnSimulationStepUpdate(float step)
    {
        _simualtionStep = (int)step;
    }

    private void UpdatePeriodUI()
    {
        long years = _simualationPeriod / 12;
        if (years != 0)
        {
            long month = _simualationPeriod % (years*12);
            if (month >= 10)
            {
                _timeText.text = "Year: " + years + " month " + month;
            }
            else
            {
                _timeText.text = "Year: " + years + " month " + month + " ";
            }
            
        }
        else
        {
            _timeText.text = "Month: " + _simualationPeriod;
        }
    }

    public void UpdateColonies()
    {
        for (int i=0; i<colonies.Count; ++i)
        {
            colonies[i].UpdateColonyUI(_simulationController.getColony());

            double max = 40001877097;
            double mass = _simulationController.getColony().getMass();
            double massPercent = System.Math.Sqrt(mass) / System.Math.Sqrt(max);
            double radius = 0.3 + 14.7 * massPercent;
            if (mass > 30001877097)
            {
                colonies[i].SetSize((float)radius);
            }
            else if (!_simulationController.getColony().isDead())
            {
                colonies[i].SetSize((float)radius);
            }
            else
            {
                colonies[i].SetSize(0.3f);
                colonies[i].SetDead();
            }
            

        }
    }

    public void UpdatePlanetInfo()
    {
        _temperatureText.text = "Temperature: " + getTemperature() + "C";
        _pressureText.text = "Pressure: " + GetPressure()+"atm";
        _radiationText.text = "Radiation: " + GetRadiation()+"mGy/day";
        _CO2Text.text = "CO2: " + GetCO2() + "%";
        _O2Text.text = "O2: " + GetO2() + "%";
        _N2atmosphereText.text = "N2 (atmosphere): " + GetN() + "%";
        _N2groundText.text = "N (ground): " + GetNground();
        _organicText.text = "Organic: " + GetOrganic();
    }

    private double getTemperature()
    {
        return _simulationController.getConditions().getTemperature();
    }

    private double GetPressure()
    {
        return 0.006280;
    }

    private double GetRadiation()
    {
        return 210;
    }

    private double GetCO2()
    {
        return System.Math.Round(_simulationController.getConditions().getCO() * 100, 3);
    }

    private double GetO2()
    {
        return System.Math.Round(_simulationController.getConditions().getO2() * 100, 3);
    }

    private double GetN()
    {
        return System.Math.Round(_simulationController.getConditions().getN() * 100, 3);
    }

    private double GetNground()
    {
        return _simulationController.getConditions().getGroundN();
    }

    private double GetOrganic()
    {
        return _simulationController.getConditions().GetOrganic();
    }
}
