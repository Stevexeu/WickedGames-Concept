using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerCore : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] public InputHandler inputHandler;

    [Header("References")]
    [SerializeField] public Transform playerPos;
    Rigidbody physicsBody;
    public Animator animator;

    [Header("Movement")]
    [SerializeField] public float walkSpeed = 11f;
    [SerializeField] public float crouchSpeed = 4f;
    [SerializeField] public float movementSpeed = 0f;
    [SerializeField] public float gravityDrag = 1f;
    [SerializeField] public float jumpHeight = 100f;
    [SerializeField] public float normalHeight, crouchHeight;
    private Vector2 currentSpeed;

    [Header("Booleans")]
    [SerializeField] public bool isGrounded = false;
    [SerializeField] public bool isWalking = false;

    private void Start()
    {
        movementSpeed = walkSpeed;
        physicsBody = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if (currentSpeed.x == 0 || currentSpeed.y == 0 | currentSpeed.x == 0 || currentSpeed.y == 0)
        {
            isWalking = false;
            animator.SetBool("isWalking", false);
        }

        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = movementSpeed * m_Input;
        velocity.y = physicsBody.velocity.y;
        physicsBody.velocity = velocity;
        currentSpeed.x = (Input.GetAxisRaw("Vertical"));
        currentSpeed.y = (Input.GetAxisRaw("Horizontal"));

    }

    private void Update()
    {
        if (currentSpeed.x > 0 | currentSpeed.y > 0 | currentSpeed.x < 0 | currentSpeed.y < 0)
        {
            if (Input.GetAxisRaw("Vertical") < 0 | Input.GetAxisRaw("Vertical") > 0 | Input.GetAxisRaw("Horizontal") < 0 | Input.GetAxisRaw("Horizontal") > 0)
            {
                isWalking = true;
                animator.SetBool("isWalking", true);
                animator.SetFloat("Speed", currentSpeed.magnitude);
                animator.SetFloat("Vertical", currentSpeed.x);
                animator.SetFloat("Horizontal", currentSpeed.y);
            }
        }
        animator.SetFloat("Speed", currentSpeed.magnitude);
    }

    public void Jump()
    {
        animator.SetTrigger("ShouldJump");
        physicsBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
    }

    public void StartCrouch()
    {
        movementSpeed = crouchSpeed;
        animator.SetBool("ShouldCrouch", true);
    }

    public void StopCrouch()
    {
        movementSpeed = walkSpeed;
        animator.SetBool("ShouldCrouch", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("IsGrounded", true);
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("IsGrounded", false);
        isGrounded = false;
    }
}
