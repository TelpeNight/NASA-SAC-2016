using UnityEngine;
using UnityEngine.SceneManagement;
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
    }

    void UpdateSliderValue(SliderGroup slider, float value)
    {
        slider.SetValue(value);
    }

    public void StartSimulation()
    {
        model.ColonyParams modelParams = new model.ColonyParams();
        modelParams.radiationResistent = 0.98f + _bacteria.radiationResist / 1000;
        modelParams.optimalTemperature = (int)_bacteria.temperatureResist;
        modelParams.photosynthesisPower = _bacteria.photosynthesisPower / 100;
        modelParams.nFixPower = _bacteria.nitrogenPower / 100;
        modelParams.cellSize = _bacteria.maxSize;
        controller.ParamsController.colonyParams = modelParams;
        controller.ParamsController.growthSpeed = (int)_bacteria.divisionSpeed;
        SceneManager.LoadScene("simulation");
    }

    public void Revert()
    {
        _bacteria.reset();
    }
}
