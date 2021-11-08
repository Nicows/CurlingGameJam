using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider sliderForce;
    public PlayerMovements player;

    void Start()
    {
        sliderForce.minValue = player.minForceKick;
        sliderForce.maxValue = player.maxForceKick;
    }

    // Update is called once per frame
    void Update()
    {
        sliderForce.value = player.currentForceKick;
    }
}
