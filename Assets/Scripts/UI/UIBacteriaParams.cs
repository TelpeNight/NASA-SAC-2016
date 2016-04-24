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
        InvokeValueChanged();
    }

    public void OnTemperatureResistUpdate(float value)
    {
        temperatureResist = value;
        InvokeValueChanged();
    }

    public void OnMaxSizeUpdate(float value)
    {
        maxSize = value;
        InvokeValueChanged();
    }

    public void OnPhotosynthesisPowerUpdate(float value)
    {
        photosynthesisPower = value;
        InvokeValueChanged();
    }

    public void OnNitrogenPowerUpdate(float value)
    {
        nitrogenPower = value;
        InvokeValueChanged();
    }

    public void OnDivisionSpeedUpdate(float value)
    {
        divisionSpeed = value;
        InvokeValueChanged();
    }

    public void OnWaterUsageUpdate(float value)
    {
        waterUsage = value;
        InvokeValueChanged();
    }
}
