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
    [SerializeField] public float walkSpeed = 5f;
    [SerializeField] public float gravityDrag = 1f;
    [SerializeField] public float jumpHeight = 100f;
    private Vector2 currentSpeed;

    [Header("Booleans")]
    [SerializeField] public bool isGrounded = false;

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
        currentSpeed.x = (Input.GetAxisRaw("Vertical"));
        currentSpeed.y = (Input.GetAxisRaw("Horizontal"));

    }

    private void Update()
    {
        if (currentSpeed.x > 0 | currentSpeed.y > 0 | currentSpeed.x < 0 | currentSpeed.y < 0)
        {
            if (Input.GetAxisRaw("Vertical") < 0 | Input.GetAxisRaw("Vertical") > 0 | Input.GetAxisRaw("Horizontal") < 0 | Input.GetAxisRaw("Horizontal") > 0)
            {
                animator.SetFloat("Speed", currentSpeed.magnitude);
                animator.SetFloat("Vertical", currentSpeed.x);
                animator.SetFloat("Horizontal", currentSpeed.y);
            }
        }
        animator.SetFloat("Speed", currentSpeed.magnitude);
    }

    public void Jump()
    {
        physicsBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("IsGrounded", false);
        animator.SetTrigger("ShouldJump");
        isGrounded = false;
    }
}
