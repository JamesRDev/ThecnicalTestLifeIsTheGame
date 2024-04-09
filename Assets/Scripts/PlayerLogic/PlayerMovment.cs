using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speedCharacter;
    [SerializeField] private float gravity = 9.81f; 
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcula la dirección del movimiento basada en los ejes horizontal y vertical
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // Aplica la velocidad de movimiento horizontal
        movement *= speedCharacter;

        // Si el personaje está en el suelo, resetea la velocidad vertical
        if (characterController.isGrounded)
        {
            moveDirection.y = 0f;
        }

        // Aplica gravedad
        moveDirection.y -= gravity * Time.deltaTime;

        // Combina el movimiento horizontal y vertical
        movement += moveDirection;

        // Transforma la dirección del movimiento a la dirección local del personaje
        movement = transform.TransformDirection(movement);

        // Mueve al personaje
        characterController.Move(movement * Time.deltaTime);
    }

    private void MoveCharacter(Vector3 movment)
    {
        movment = transform.TransformDirection(movment);
        characterController.Move(movment * Time.deltaTime);
    }
}
