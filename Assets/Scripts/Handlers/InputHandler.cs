using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Referebces")]
    public PlayerCore playerCore;

    [Header("Movement Inputs")]
    [SerializeField] public KeyCode walkForward = KeyCode.W;
    [SerializeField] public KeyCode walkLeft = KeyCode.A;
    [SerializeField] public KeyCode walkBack = KeyCode.S;
    [SerializeField] public KeyCode walkRight = KeyCode.D;
    [SerializeField] public KeyCode jump = KeyCode.Space;
    [SerializeField] public KeyCode crouch = KeyCode.C;
    [SerializeField] public KeyCode run = KeyCode.LeftShift;

    [Header("Interaction Inputs")]
    [SerializeField] public KeyCode openInventory = KeyCode.I;

    private void Update()
    {
        if (Input.GetKeyDown(jump) && playerCore.isGrounded && playerCore.isCrouching == false)
            playerCore.Jump();
        if (Input.GetKeyDown(crouch) && playerCore.isGrounded)
            playerCore.StartCrouch();
        if (Input.GetKeyUp(crouch) && playerCore.isGrounded)
            playerCore.StopCrouch();
        if (Input.GetKeyDown(run) && playerCore.isGrounded && playerCore.isWalking && playerCore.isCrouching == false)
            playerCore.StartRunning();
        if (Input.GetKeyUp(run) && playerCore.isGrounded)
            playerCore.StopRunning();
    }

    private void FixedUpdate()
    {

    }
}
