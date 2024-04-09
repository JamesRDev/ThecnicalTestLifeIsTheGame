using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimeVortex : MonoBehaviour
{
    private float totalTimeInAir;
    private float currentTime = 0f;
    private float initialSpeed;
    private float initialBulletSpeed = 5f;
    private float gravity;
    private Vector3 direction;
    private Vector3 initialPosition;
    private GameObject tornadoPrefab;
    public void Initialize(Vector3 dir, float initialSpeed, float gravity, Vector3 initialPos, GameObject TornadoLight)
    {
        direction = dir.normalized;
        this.initialSpeed = initialSpeed;
        this.gravity = gravity;
        this.tornadoPrefab = TornadoLight;
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
            StartCoroutine(DestroyBullet());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión es con otro objeto
        if (collision.gameObject.CompareTag("Vortex"))
        {
            // Instanciar el tornado en la posición de la colisión
            Instantiate(tornadoPrefab, collision.contacts[0].point, Quaternion.identity);

            // Destruir la bala
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyBullet() {

        yield return new WaitForSeconds(1);
        // Destroy the bullet when it has completed its trajectory
        Destroy(gameObject);

    }
}

