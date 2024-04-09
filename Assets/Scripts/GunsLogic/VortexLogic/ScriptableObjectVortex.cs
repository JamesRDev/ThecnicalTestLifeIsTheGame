using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Parameters", menuName = "Guns/Vortex Gun")]


public class ScriptableObjectVortex : ScriptableObject
{

    [Tooltip("Size of the charger")]
    [SerializeField] private int chargerSize;


    [Tooltip("Initial speed of the shoot")]
    [SerializeField] private float initialSpeedShoot;


    [Tooltip("Attraction force")]
    [SerializeField] private float attractionForce;


    [Tooltip("Duration of the vortex")]
    [SerializeField] private float vortexDuration;


    [Tooltip("Radius of the vortex")]
    [SerializeField] private float vortexRadius;

    [Tooltip("Color of the vortex")]

    [SerializeField] private Color vortexColor;
    public int GetChargerSiezeValue()
    {
        return chargerSize;
    }
    public float GetInitialSpeedShootValue()
    {
        return initialSpeedShoot;
    }
    public float GetAttractionForceValue()
    {
        return attractionForce;
    }
    public float GetVortexDurationValue()
    {
        return vortexDuration;
    }
    public float GetVortexRadiusValue()
    {
        return vortexRadius;
    }

    public Color GetColorVortexValue()
    {

        return vortexColor;
    }
}
