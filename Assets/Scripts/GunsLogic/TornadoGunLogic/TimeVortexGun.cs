using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeVortexGun : MonoBehaviour
{


    [SerializeField] private ScriptableObjectTornado scVortex;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Transform shootingPoint;

    private Vector3 direction;
    private float initialSpeed;
    private float gravity;
    private int chargerSize;
    private GameObject tornadoLight;
    private void Start()
    {
        initialSpeed = scVortex.GetInitialSpeedShootValue();
        gravity = scVortex .GetGravityValue();
        chargerSize = scVortex.GetChargerSizeValue();
        tornadoLight = scVortex.GetTornadoPref();

    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && chargerSize > 0)
{
            chargerSize--;
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            Vector3 direction = ray.direction;
            Vector3 spawnPosition = shootingPoint.position;
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                spawnPosition = ray.GetPoint(100); 
            }
            else
            {
                direction = (hit.point - shootingPoint.position).normalized;
            }

            // Crear la bala
            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            BulletTimeVortex bullet = newBullet.GetComponent<BulletTimeVortex>();
            bullet.Initialize(direction, initialSpeed, gravity, shootingPoint.position, tornadoLight);
        }
    }
}
