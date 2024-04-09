using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParabolicSistem : MonoBehaviour
{
    [SerializeField] private ParabolicScriptableObject scParabolic;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Transform shootingPoint;

    private Vector3 direction;
    private float initialSpeed;
    private float gravity;
    private int chargerSize;
    private void Start()
    {
        initialSpeed = scParabolic.GetChargerSizeValue();
        gravity = scParabolic.GetGravityValue();
        chargerSize = scParabolic.GetChargerSizeValue();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            ParabolicBulletSystem bullet = newBullet.GetComponent<ParabolicBulletSystem>();
            bullet.Initialize(direction, initialSpeed, gravity, shootingPoint.position);
        }
    }
}