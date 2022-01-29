using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampReloadBar : MonoBehaviour
{
    [SerializeField] private Slider reloadSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 sliderPos = Camera.main.WorldToScreenPoint(transform.position);
        reloadSlider.transform.position = sliderPos;
    }
}
