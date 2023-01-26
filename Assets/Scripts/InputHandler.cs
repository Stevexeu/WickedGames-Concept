using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Referebces")]
    public PlayerCore playerCore;
    private Vector3 _movement;

    [Header("Movement Inputs")]
    [SerializeField] public KeyCode walkForward = KeyCode.W;
    [SerializeField] public KeyCode walkLeft = KeyCode.A;
    [SerializeField] public KeyCode walkBack = KeyCode.S;
    [SerializeField] public KeyCode walkRight = KeyCode.D;

    [Header("Interaction Inputs")]
    [SerializeField] public KeyCode openInventory = KeyCode.I;

    private void Update()
    {
        float inputY = 0;
        if (Input.GetKey(KeyCode.D))
            inputY = 1;
        else if (Input.GetKey(KeyCode.A))
            inputY = -1;

        // Horizontal
        float inputX = 0;
        if (Input.GetKey(KeyCode.D))
        {
            inputX = 1;
            playerCore.playerPos.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputX = -1;
            playerCore.playerPos.localScale = new Vector3(-1, 1, 1);
        }
        _movement = new Vector3(inputX, 0, inputY).normalized;
    }
}
