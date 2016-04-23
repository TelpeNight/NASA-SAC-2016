using UnityEngine;
using System.Collections.Generic;

public class EditorUIController : MonoBehaviour
{
    public UIBacteriaParams _bacteria;
    public SliderGroup radiationSlider;
    public SliderGroup temperatureSlider;
    public SliderGroup maxSizeSlider;
    public SliderGroup photoSlider;
    public SliderGroup nitrogenSlider;
    public SliderGroup divisionSpeedSlider;
    public SliderGroup waterUseSlider;


    // Use this for initialization
    void Start()
    {
        _bacteria.valueChanged += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateUI()
    {
        UpdateSliderValue(radiationSlider, _bacteria.radiationResist);
        UpdateSliderValue(temperatureSlider, _bacteria.temperatureResist);
        UpdateSliderValue(maxSizeSlider, _bacteria.maxSize);
        UpdateSliderValue(photoSlider, _bacteria.photosynthesisPower);
        UpdateSliderValue(nitrogenSlider, _bacteria.nitrogenPower);
        UpdateSliderValue(divisionSpeedSlider, _bacteria.divisionSpeed);
        UpdateSliderValue(waterUseSlider, _bacteria.waterUsage);
    }

    void UpdateSliderValue(SliderGroup slider, float value)
    {
        slider.SetValue(value);
    }

    public void StartSimulation()
    { }

    public void Revert()
    {
        _bacteria.reset();
    }
}
