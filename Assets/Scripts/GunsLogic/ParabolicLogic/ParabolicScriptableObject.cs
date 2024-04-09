using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gun Parameters", menuName = "Guns/ParabolicGun")]
public class ParabolicScriptableObject : ScriptableObject
{
    [Tooltip("Size of the charger.")]
    [SerializeField] private int chargerSize;

    [Tooltip("Initial speed of the shoot.")]
    [SerializeField] private float initialSpeed;

    [Tooltip("Strength of gravity affecting the shoot.")]
    [SerializeField] private float gravity;

  
    public int GetChargerSizeValue()
    {
        return chargerSize;
    }

  
    public float GetInitialSpeedValue()
    {
        return initialSpeed;
    }

   
    public float GetGravityValue()
    {
        return gravity;
    }
}
