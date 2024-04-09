using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagerUi : MonoBehaviour

{
    [SerializeField] private GameObject interactiveOptionPrefab;
    [SerializeField] private Transform interactionSpawnPoint;
    [SerializeField] private GameObject[] guns;
    [SerializeField] private GameObject[] gunPrefabsDrop;
    [SerializeField] private Transform dropPivot;
    [SerializeField] private TextMeshProUGUI weaponNameText; 
    private GameObject currentGun;
    private bool isInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            interactiveOptionPrefab.SetActive(true);
            currentGun = other.gameObject;
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            interactiveOptionPrefab.SetActive(false);
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Recolectar();
        }
    }

    private void Recolectar()
    {
        if (currentGun != null)
        {
            string currentGunName = currentGun.name;
            int index = System.Array.FindIndex(guns, x => x.name == currentGunName);
            if (index != -1)
            {
                DesactivarArmas();
                InstanciarArmaDesactivada(currentGunName);
                ActivarArma(index);
                UpdateWeaponNameText(currentGunName); 
            }
            else
            {
                Debug.LogWarning("The name of the collected weapon is not in the list of active weapons: " + currentGunName);
            }
        }
    }

    private void DesactivarArmas()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }

    private void InstanciarArmaDesactivada(string gunName)
    {
        GameObject droppedGun = Instantiate(GetGunPrefab(gunName), dropPivot.position, dropPivot.rotation);
    }

    private void ActivarArma(int index)
    {
        guns[index].SetActive(true);
        currentGun = guns[index];
    }
    private void UpdateWeaponNameText(string weaponName)
    {
        if (weaponNameText != null)
        {
            weaponNameText.text = weaponName;
        }
    }
    private GameObject GetGunPrefab(string gunName)
    {
        foreach (GameObject prefab in gunPrefabsDrop)
        {
            if (prefab.name == gunName)
            {
                return prefab;
            }
        }
        return null;
    }
}



