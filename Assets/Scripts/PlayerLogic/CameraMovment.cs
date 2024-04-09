using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float mouseSendibility;
    [SerializeField] private float rotationLimitTop;
    [SerializeField] private float rotationLimitBottom;
    [SerializeField] private float rotationSmoothnes;

    [SerializeField] private float rotationHorizontal;
    [SerializeField] private float rotationVertical;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        float horizontalMovment= Input.GetAxis("Mouse X") * rotationSpeed * mouseSendibility;
        float verticalMovment = Input.GetAxis("Mouse Y") * rotationSpeed * mouseSendibility;

        rotationHorizontal += horizontalMovment;
        rotationVertical -= verticalMovment;

        rotationVertical = Mathf.Clamp(rotationVertical, rotationLimitBottom, rotationLimitTop);

        Quaternion objectRotation = Quaternion.Euler(rotationVertical, rotationHorizontal, 0);
        pivot.rotation = Quaternion.Lerp(pivot.rotation,objectRotation,rotationSmoothnes * Time.deltaTime);
    }
}
