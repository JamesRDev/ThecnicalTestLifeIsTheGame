using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexGun : MonoBehaviour
{
    [SerializeField] private ScriptableObjectVortex scriptableVortex;
    [SerializeField] private Transform gunFirePoint;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject bulletPrefab;
    private int chargerSize;
    private float initialSpeedShoot;
    private float attractionForce;
    private float vortexDuration;
    private float vortexRadius;
    private Color vortexColor;

    private void Start()
    {
        chargerSize = scriptableVortex.GetChargerSiezeValue();
        initialSpeedShoot = scriptableVortex.GetInitialSpeedShootValue();
        attractionForce = scriptableVortex.GetAttractionForceValue();
        vortexDuration = scriptableVortex.GetVortexDurationValue();
        vortexRadius = scriptableVortex.GetVortexRadiusValue();
        vortexColor = scriptableVortex.GetColorVortexValue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            Shoot();
            chargerSize--;
        }
    }

    private void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 fireDirection = (hit.point - gunFirePoint.position).normalized;
            CreateBullet(gunFirePoint.position, fireDirection);
        }
    }

    private void CreateBullet(Vector3 initialPosition, Vector3 direction)
    {
        GameObject bulletObject = Instantiate(bulletPrefab, initialPosition, Quaternion.identity);
        BulletVortex bullet = bulletObject.GetComponent<BulletVortex>();
        if (bullet != null)
        {
            bullet.Initialize(attractionForce, vortexDuration, vortexRadius, vortexColor);
            bulletObject.GetComponent<Rigidbody>().velocity = direction * initialSpeedShoot;
        }
    }
}
