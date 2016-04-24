using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ColonyUI : MonoBehaviour
{
    public List<UIColonyParams> _colonies = new List<UIColonyParams>();
    public Canvas canvas;
    public GameObject sprite;
    private SphereCollider _collider;

    private Text _totalMassText;
    private Text _radiationResistanceText;
    private Text _temperatureResistanceText;
    private Text _maxSizeText;
    private Text _divisionPeriodText;
    private Text _photosynthesisPowerText;
    private Text _nitrogenPowerText;

    // Use this for initialization
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _totalMassText = canvas.transform.FindChild("Mass").GetComponent<Text>();
        _radiationResistanceText = canvas.transform.FindChild("Radiation").GetComponent<Text>();
        _temperatureResistanceText = canvas.transform.FindChild("Temperature").GetComponent<Text>();
        _maxSizeText = canvas.transform.FindChild("MaxSize").GetComponent<Text>();
        _photosynthesisPowerText = canvas.transform.FindChild("PhotosynthesisPower").GetComponent<Text>();
        _nitrogenPowerText = canvas.transform.FindChild("NitrogenPower").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateColonyUI(model.Colony colonyInfo)
    {
        _totalMassText.text = "Total mass: " + GetTotalMass(colonyInfo);
        _radiationResistanceText.text = "Radiation resistance: " + GetRadiationResistance(colonyInfo);
        _temperatureResistanceText.text = "Optimal temperature: " + GetTemperatureResistance(colonyInfo);
        _maxSizeText.text = "Size: " + GetMaxSize(colonyInfo);
        _photosynthesisPowerText.text = "Photosynthesis power: " + GetPhotoPower(colonyInfo);
        _nitrogenPowerText.text = "Nytrogen power: " + GetNitrogenPower(colonyInfo);
    }

    public void SetDead()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private double GetTotalMass(model.Colony colonyInfo)
    {
        return colonyInfo.getMass();
    }

    private double GetRadiationResistance(model.Colony colonyInfo)
    {
        return System.Math.Round(colonyInfo.getRadiationResistent(), 5);
    }

    private double GetTemperatureResistance(model.Colony colonyInfo)
    {
        return colonyInfo.getOptimalTemperature();
    }

    private double GetMaxSize(model.Colony colonyInfo)
    {
        return colonyInfo.getCellSize();
    }

    private double GetPhotoPower(model.Colony colonyInfo)
    {
        return System.Math.Round(colonyInfo.getPhotosynthesisPower(), 5);
    }

    private double GetNitrogenPower(model.Colony colonyInfo)
    {
        return System.Math.Round(colonyInfo.getNFixPower(), 5);
    }

    public void SetSize(float size)
    {
        sprite.transform.localScale = Vector3.one * size;
        _collider.radius = size / 4;
    }

    void OnMouseEnter()
    {
        canvas.enabled = true;
    }

    void OnMouseExit()
    {
        canvas.enabled = false;
    }
}
