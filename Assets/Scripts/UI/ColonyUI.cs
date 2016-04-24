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
        _divisionPeriodText = canvas.transform.FindChild("DivisionPeriod").GetComponent<Text>();
        _photosynthesisPowerText = canvas.transform.FindChild("PhotosynthesisPower").GetComponent<Text>();
        _nitrogenPowerText = canvas.transform.FindChild("NitrogenPower").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //float size = sprite.transform.localScale.x;
        //size += 0.1f * Time.deltaTime;
        //SetSize(size);
    }

    public void UpdateColonyUI()
    {
        _totalMassText.text = "Total mass: " + GetTotalMass();
        _radiationResistanceText.text = "Radiation resistance: " + GetRadiationResistance();
        _temperatureResistanceText.text = "Temperature resistance: " + GetTemperatureResistance();
        _maxSizeText.text = "Max size: " + GetMaxSize();
        _divisionPeriodText.text = "Division period: " + GetDivisionPeriod() + "h";
        _photosynthesisPowerText.text = "Photosynthesis power: " + GetPhotoPower();
        _nitrogenPowerText.text = "Nytrogen power: " + GetNitrogenPower();

    }

    public void SetDead()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private double GetTotalMass()
    {
        return 50;
    }

    private double GetRadiationResistance()
    {
        return 1;
    }

    private double GetTemperatureResistance()
    {
        return 2;
    }

    private double GetMaxSize()
    {
        return 3;
    }

    private double GetPhotoPower()
    {
        return 4;
    }

    private double GetNitrogenPower()
    {
        return 5;
    }

    private double GetDivisionPeriod()
    {
        return 6;
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
