using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider PosSlider;
    public Slider RotSlider;

    private GameObject obj;
    private Vector3 startPos;
    private float previousValue;

    private void Awake()
    {
        obj = GameObject.FindGameObjectWithTag("Project");

        startPos = obj.transform.position;

        RotSlider.onValueChanged.AddListener(OnRotSliderChanged);
        previousValue = RotSlider.value;
    }

    public void setObjectHeight()
    {
        obj.transform.position = new Vector3(obj.transform.position.x, startPos.y + PosSlider.value ,obj.transform.position.z);
    }

    void OnRotSliderChanged(float value)
    {
        float delta = value - previousValue;
        obj.transform.Rotate(0, delta * 360 ,0);

        previousValue = value;
    }

    public void ResetValues()
    {
        PosSlider.value = 0;
        RotSlider.value = 0;
    }
}