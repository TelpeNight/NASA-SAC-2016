using UnityEngine;
using System;

public class UIBacteriaParams : MonoBehaviour {

    public float radiationResist;
    public float temperatureResist;
    public float maxSize;
    public float photosynthesisPower;
    public float nitrogenPower;
    public float divisionSpeed;
    public float waterUsage;

    public Action valueChanged;

	// Use this for initialization
	void Start () {
        reset();  
    }

    public void reset()
    {
        radiationResist = 450f;
        temperatureResist = 0f;
        maxSize = 5f;
        photosynthesisPower = 3f;
        nitrogenPower = 1f;
        divisionSpeed = 12f;
        waterUsage = 0.5f;
        InvokeValueChanged();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InvokeValueChanged()
    {
        if (valueChanged != null)
        {
            valueChanged();
        }
    }

    public void OnRadiationResistUpdate(float value)
    {
        radiationResist = value;
    }

    public void OnTemperatureResistUpdate(float value)
    {
        temperatureResist = value;
    }

    public void OnMaxSizeUpdate(float value)
    {
        maxSize = value;
    }

    public void OnPhotosynthesisPowerUpdate(float value)
    {
        photosynthesisPower = value;
    }

    public void OnNitrogenPowerUpdate(float value)
    {
        nitrogenPower = value;
    }

    public void OnDivisionSpeedUpdate(float value)
    {
        divisionSpeed = value;
    }

    public void OnWaterUsageUpdate(float value)
    {
        waterUsage = value;
    }
}
