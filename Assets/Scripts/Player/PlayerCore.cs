using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;

    [Header("Scripts")]
    [SerializeField] public InputHandler inputHandler;

    [Header("References")]
    [SerializeField] public Transform playerPos;
    Rigidbody physicsBody;
    public Animator animator;

    [Header("Movement")]
    [SerializeField] public float runSpeed = 15f;
    [SerializeField] public float walkSpeed = 10f;
    [SerializeField] public float crouchSpeed = 8f;
    [SerializeField] public float movementSpeed = 0f;
    private Vector2 currentSpeed;

    [Header("Jumping")]
    [SerializeField] public float jumpSpeed = 100f;
    [SerializeField] public float gravityScale = 10f;
    [SerializeField] public float fallingGravityScale = 30f;
    private float currentGravityScale;

    [Header("Health & Experience")]
    [SerializeField] public int Health;
    [SerializeField] public int Exp;

    [Header("Booleans")]
    [SerializeField] public bool isWalking = false;
    [SerializeField] public bool isGrounded = true;
    [SerializeField] public bool isCrouching = false;
    [SerializeField] public bool canMove = true;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        movementSpeed = walkSpeed;
        currentGravityScale = gravityScale;
        physicsBody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (physicsBody.velocity.y >= 0)
            currentGravityScale = gravityScale;
        else if (physicsBody.velocity.y < 0)
            currentGravityScale = fallingGravityScale;
        if (physicsBody.velocity.y <= -0.12)
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
        }

        else if (physicsBody.velocity.y >= 0.12)
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false);
        }

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

    private void FixedUpdate()
    {
        if (currentSpeed.x == 0 || currentSpeed.y == 0 | currentSpeed.x == 0 || currentSpeed.y == 0)
        {
            isWalking = false;
            animator.SetBool("isWalking", false);
        }

        if (canMove)
        {
            Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 velocity = movementSpeed * m_Input;
            velocity.y = physicsBody.velocity.y;
            physicsBody.velocity = velocity;
            currentSpeed.x = (Input.GetAxisRaw("Vertical"));
            currentSpeed.y = (Input.GetAxisRaw("Horizontal"));
        }

        physicsBody.AddForce(Physics.gravity * (gravityScale - 1) * physicsBody.mass);
    }

    public void Jump()
    {
        if (canMove)
            physicsBody.AddForce(Vector2.up * jumpSpeed, ForceMode.Impulse);
    }

    public void StartRunning()
    {
        if (canMove)
        {
            movementSpeed = runSpeed;
            animator.SetBool("ShouldRun", true);
        }
    }

    public void StopRunning()
    {
        movementSpeed = walkSpeed;
        animator.SetBool("ShouldRun", false);
    }

    public void StartCrouch()
    {
        if (canMove)
        {
            isCrouching = true;
            movementSpeed = crouchSpeed;
            animator.SetBool("ShouldCrouch", true);
        }
    }

    public void StopCrouch()
    {
        isCrouching = false;
        movementSpeed = walkSpeed;
        animator.SetBool("ShouldCrouch", false);
    }

    public void IncreaseHealth(int value)
    {
        Health += value;
    }

    public void IncreaseExp(int value)
    {
        Exp += value;
    }
}
