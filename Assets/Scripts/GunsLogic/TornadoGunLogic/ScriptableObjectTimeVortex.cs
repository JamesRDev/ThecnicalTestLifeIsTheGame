using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Parameters", menuName = "Guns/Storm Light")]



public class ScriptableObjectTimeVortex : ScriptableObject
{
    [Header("Attraction Parameters")]
    [Tooltip("Attraction force applied to objects within the tornado.")]
    [SerializeField] private float attractionForce;
    [Tooltip("Radius within which objects are attracted to the tornado.")]
    [SerializeField] private float attractionRadius;

    [Header("Rotation Parameters")]
    [Tooltip("Speed at which the tornado rotates around its axis.")]
    [SerializeField] private float rotationSpeed;

    [Header("Movement Parameters")]
    [Tooltip("Initial ascending speed of the tornado.")]
    [SerializeField] private float initialSpeed;
    [Tooltip("Height at which the tornado self-destructs.")]
    [SerializeField] private float destroyHeight;
    [Tooltip("Force applied to objects upon collision for bouncing effect.")]
    [SerializeField] private float bounceForce;

    [Header("Explosion Parameters")]
    [Tooltip("Force applied to nearby objects upon tornado's destruction.")]
    [SerializeField] private float explosionForce;

    [Header("Layer Settings")]
    [Tooltip("Layer mask defining which layers attract objects.")]
    [SerializeField] private LayerMask attractionLayer;



    public float GetAttractionForce()
    {
        return attractionForce;
    }


    public float GetAttractionRadius()
    {
        return attractionRadius;
    }

   
    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }

    public float GetInitialSpeed()
    {
        return initialSpeed;
    }

    public float GetDestroyHeight()
    {
        return destroyHeight;
    }

  
    public float GetBounceForce()
    {
        return bounceForce;
    }
 
    public float GetExplosionForce()
    {
        return explosionForce;
    }

    public LayerMask GetAttractionLayer()
    {
        return attractionLayer;
    }
}
