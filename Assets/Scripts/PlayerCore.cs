using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] public InputHandler inputHandler;

    [Header("References")]
    [SerializeField] public Transform playerPos;
    Rigidbody physicsBody;

    [Header("Input")]


    [Header("Movement")]
    [SerializeField] public float walkSpeed = 5f;
    private Vector3 _movement;


    private void Start()
    {
        physicsBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = walkSpeed * m_Input;
        velocity.y = physicsBody.velocity.y;
        physicsBody.velocity = velocity;

    }

    public void Jump()
    {

    }
}
