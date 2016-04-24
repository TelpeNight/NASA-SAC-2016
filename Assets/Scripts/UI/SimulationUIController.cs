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
    private long _simualtionStep = 1;
    private UnityEngine.UI.Text _timeText;


    // Use this for initialization
    void Start()
    {
        _temperatureText = screenCanvas.transform.FindChild("Temperature").GetComponent<UnityEngine.UI.Text>();
        _pressureText = screenCanvas.transform.FindChild("Pressure").GetComponent<UnityEngine.UI.Text>();
        _radiationText = screenCanvas.transform.FindChild("Radiation").GetComponent<UnityEngine.UI.Text>();
        _humidityText = screenCanvas.transform.FindChild("Humidity").GetComponent<UnityEngine.UI.Text>();
        _CO2Text = screenCanvas.transform.FindChild("CO2").GetComponent<UnityEngine.UI.Text>();
        _O2Text = screenCanvas.transform.FindChild("O2").GetComponent<UnityEngine.UI.Text>();
        _N2atmosphereText = screenCanvas.transform.FindChild("N").GetComponent<UnityEngine.UI.Text>();
        _N2groundText = screenCanvas.transform.FindChild("Nground").GetComponent<UnityEngine.UI.Text>();
        _organicText = screenCanvas.transform.FindChild("Organic").GetComponent<UnityEngine.UI.Text>();
        _timeText = screenCanvas.transform.FindChild("Timer").GetComponent<UnityEngine.UI.Text>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseSimulationPeriod();
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
        Debug.Log((int)step);
        _simualtionStep = (int)step;
    }

    private void UpdatePeriodUI()
    {
        long years = _simualationPeriod / 12;
        if (years != 0)
        {
            long month = _simualationPeriod % years;
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
            colonies[i].UpdateColonyUI();
        }
    }

    public void UpdatePlanetInfo()
    {
        _temperatureText.text = "Temperature: " + getTemperature() + "C";
        _pressureText.text = "Pressure: " + GetPressure();
        _radiationText.text = "Radiation: " + GetRadiation();
        _humidityText.text = "Humidity: " + GetHumidity();
        _CO2Text.text = "CO2: " + GetCO2() + "%";
        _O2Text.text = "O2: " + GetO2() + "%";
        _N2atmosphereText.text = "N2 (atmosphere): " + GetN() + "%";
        _N2groundText.text = "N (ground)" + GetNground();
        _organicText.text = "Organic " + GetOrganic();
    }

    private double getTemperature()
    {
        return 1;
    }

    private double GetPressure()
    {
        return 2;
    }

    private double GetRadiation()
    {
        return 3;
    }

    private double GetHumidity()
    {
        return 4;
    }

    private double GetCO2()
    {
        return 5;
    }

    private double GetO2()
    {
        return 6;
    }

    private double GetN()
    {
        return 7;
    }

    private double GetNground()
    {
        return 8;
    }

    private double GetOrganic()
    {
        return 9;
    }
}
