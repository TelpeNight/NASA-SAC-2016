using UnityEngine;
using UnityEngine.UI;

public class SliderGroup : MonoBehaviour
{
    public Slider slider;
    public Text title;
    public Text minValue;
    public Text maxValue;

    // Use this for initialization
    void Start()
    {
        slider = transform.GetComponentInChildren<Slider>();
        title = transform.FindChild("title").GetComponent<Text>();
        minValue = transform.FindChild("minValue").GetComponent<Text>();
        maxValue = transform.FindChild("maxValue").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMaxValue(float value)
    {
        maxValue.text = value.ToString();
    }

    public void SetMinValue(float value)
    {
        minValue.text = value.ToString();
    }

    public void SetTitle(string value)
    {
        title.text = value;
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
