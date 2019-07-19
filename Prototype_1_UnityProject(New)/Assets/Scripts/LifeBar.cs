using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    // Public UI References
    public Image fillImage;
   // public Text displayText;

    // Trackers for min/max values
    protected float maxValue = 2f, minValue = 0f;

    // Create a property to handle the slider's value
    private float currentValue = 0f;
    private float R= 94f;
    private float G = 94f;
    private float B = 94f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            // Ensure the passed value falls within min/max range
            currentValue = Mathf.Clamp(value, minValue, maxValue);

            // Calculate the current fill percentage and display it
            float fillPercentage = currentValue / maxValue;
            fillImage.fillAmount = fillPercentage;
           // displayText.text = (fillPercentage * 100).ToString("0.00") + "%";
        }
    }

    void Start()
    {
        CurrentValue = 1.9f;
        // CHECKKKKKK
        //lifeBar.transform.eulerAngles = new Vector3(this.transform.rotation.x, this.transform.rotation.y, newAngle);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentValue -= 0.0086f;
        if(CurrentValue > 1.3f)
            fillImage.color = new Color32(94, 248, 94, 255);
        else if (CurrentValue < 0.5f)
            fillImage.color = new Color32(236, 64, 70, 255);
        else
            fillImage.color = new Color32(255, 202, 11, 255);


    }
}
