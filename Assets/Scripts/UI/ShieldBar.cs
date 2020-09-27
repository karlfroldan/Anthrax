using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    public void SetShield(float shield)
    {
        slider.value = shield;
    }
}
