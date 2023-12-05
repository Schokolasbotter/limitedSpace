using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private inputManager inputManager;
    private CharacterController controller;
    private Animator animator;

    [Header("Movement Variables")]
    private Vector2 movementVector = Vector2.zero;
    private Vector3 playerVelocity = Vector3.zero;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    private float currentSpeed = 0f;
    private bool isRunning;
    public float jumpHeight = 5f;
    private Vector3 jumpDirection = Vector3.zero;
    public float gravity = 9.81f;
    public bool isGrounded = false;

    private void Start()
    {
        //Get Components
        inputManager = FindObjectOfType<inputManager>();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        //Subscribe To Input Events
        inputManager.OnJump.AddListener(playerJump);
    }

    private void Update()
    {
        //Player Movement
        playerMove();
        //Animation
        setAnimation();
    }

    public void playerMove() {
        // Get Values
        isGrounded = controller.isGrounded;
        movementVector = inputManager.movementVector;
        isRunning = inputManager.isRunning;
        //Horizontal
        Vector3 movementDirection = Vector3.forward * movementVector.y + Vector3.right * movementVector.x;
        if (movementDirection.magnitude == 0f)
        {
            currentSpeed = 0f;
        }
        else
        {
            if (isRunning)
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }

        if(isGrounded)
        {
            controller.Move(movementDirection * currentSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(jumpDirection* currentSpeed * Time.deltaTime);
        }
        //Vertical
        if (isGrounded && playerVelocity.y <= 0f)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y -= gravity*Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }    

    public void setAnimation()
    {
        animator.SetFloat("RunningSpeed", currentSpeed, 0.1f, Time.deltaTime);
    }

    //Input Functions
    public void playerJump()
    {
        if (isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravity);
            jumpDirection = Vector3.forward * movementVector.y + Vector3.right * movementVector.x;
        }
    }

    public void playerRun()
    {
        if (isGrounded)
        {
            isRunning = true;
        }
    }
}
