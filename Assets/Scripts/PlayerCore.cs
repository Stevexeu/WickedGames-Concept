using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] public InputHandler inputHandler;

    [Header("References")]
    [SerializeField] public Rigidbody physicsBody;
    [SerializeField] public Transform playerPos;
    [SerializeField] public CharacterController characterController;

    [Header("Input")]


    [Header("Movement")]
    [SerializeField] public float walkSpeed = 5f;
    private Vector3 _movement;


    private void Start()
    {
    }

    private void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * walkSpeed);
    }

    private void FixedUpdate()
    {
        physicsBody.velocity = _movement * walkSpeed;
    }
}
