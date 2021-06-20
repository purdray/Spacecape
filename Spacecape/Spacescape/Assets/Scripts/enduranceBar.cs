using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enduranceBar : MonoBehaviour
{

    public Slider enduranceSlider;
    public Gradient gradient;
    public Image fillBar;

    // Start is called before the first frame update
    void Start()
    {
        enduranceSlider.maxValue = playerMovement.initEndurance;
    }

    // Update is called once per frame
    void Update()
    {
        this.fillBar.color = gradient.Evaluate(enduranceSlider.normalizedValue);
        this.enduranceSlider.value = playerMovement.endurance;
    }
}
