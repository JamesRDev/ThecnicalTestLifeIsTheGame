using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicBulletSystem : MonoBehaviour
{
    private float totalTimeInAir;
    private float currentTime = 0f;
    private float initialSpeed;
    private float initialBulletSpeed = 5f;
    private float gravity;
    private Vector3 direction;
    private Vector3 initialPosition;
    public void Initialize(Vector3 dir, float initialSpeed, float gravity, Vector3 initialPos)
    {
        direction = dir.normalized;
        this.initialSpeed = initialSpeed;
        this.gravity = gravity;
        initialPosition = initialPos;
        totalTimeInAir = (2f * initialBulletSpeed * Mathf.Sin(Vector3.Angle(direction, Vector3.up) * Mathf.Deg2Rad)) / Mathf.Abs(gravity);
    }

    void Update()
    {
        // Calculate time elapsed since bullet was fired
        currentTime += Time.deltaTime;

        if (currentTime <= totalTimeInAir)
        {
            // Calculate bullet's position at current time using parabolic motion formula
            float x = initialSpeed * currentTime * direction.x;
            float y = initialSpeed * currentTime * direction.y + 0.5f * gravity * currentTime * currentTime;
            float z = initialSpeed * currentTime * direction.z;

            Vector3 position = initialPosition + new Vector3(x, y, z);

            // Update bullet's position
            transform.position = position;
        }
        else
        {
            // Destroy the bullet when it has completed its trajectory
            Destroy(gameObject);
        }
    }
}
