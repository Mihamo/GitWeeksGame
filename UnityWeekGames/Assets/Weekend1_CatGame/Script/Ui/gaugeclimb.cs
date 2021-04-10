using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gaugeclimb : MonoBehaviour
{
    Slider mSlider;
    Image fill;
    [SerializeField] List<Color> colorSlider;

    [SerializeField] float MaxValue;
    // Start is called before the first frame update
    void Start()
    {
        mSlider = GetComponent<Slider>();
        mSlider.maxValue = MaxValue;
        fill = mSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
    }

    public void actualiseGauge(float newValue)
    {
        mSlider.value = newValue;
        if (mSlider.normalizedValue >= 0.5f)
            fill.color = colorSlider[0];
        else if (mSlider.normalizedValue > 0.2f)
            fill.color = colorSlider[1];
        else if (mSlider.normalizedValue <= 0.2f)
            fill.color = colorSlider[2];

    }
}
