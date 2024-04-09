using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Parameters", menuName = "Guns/Vortex Time Gun")]



public class ScriptableObjectTornado : ScriptableObject
{
    [Header("BulletParameters")]
    [Tooltip("Initial speed for shoot.")]
    [SerializeField] private float initialSpeed;
  

    [Tooltip("Size of the charger.")]
    [SerializeField] private int chargerSize;

    [Tooltip("Strength of gravity affecting the shoot.")]
    [SerializeField] private float gravity;

    [Tooltip("Tornado prefap.")]
    [SerializeField] private GameObject tornadoPref;


    public int GetChargerSizeValue()
    {
        return chargerSize;
    }

  
    public float GetInitialSpeedShootValue()
    {
        return initialSpeed;
    }


    public float GetGravityValue()
    {
        return gravity;
    }

    public GameObject GetTornadoPref() 
    {
        return tornadoPref;
    }
}
