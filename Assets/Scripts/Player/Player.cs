using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    private inputManager inputManager;
    private CharacterController controller;
    private Animator animator;
    private Camera playerCamera;

    [Header("Movement Variables")]
    private Vector2 inputVector = Vector2.zero;
    private Vector3 playerVelocity = Vector3.zero;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    private float currentSpeed = 0f;
    private bool isRunning;
    public float jumpHeight = 5f;
    private Vector3 jumpDirection = Vector3.zero;
    private float gravity = 9.81f;
    private bool isGrounded = false;
    private Vector3 directionTarget;
    public float rotationSpeed = 3f;

    private void Start()
    {
        //Get Components
        inputManager = FindObjectOfType<inputManager>();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerCamera = Camera.main;
        //Subscribe To Input Events
        inputManager.OnJump.AddListener(playerJump);
    }

    private void Update()
    {
        //Player Movement
        playerMove();
        //playerRotate();
        //Animation
        setAnimation();
    }

    public void playerMove() {
        // Get Values
        isGrounded = controller.isGrounded;
        inputVector = inputManager.movementVector;
        isRunning = inputManager.isRunning;

        //Get Direction
        Vector3 movementDirection = playerCamera.transform.forward * inputVector.y + playerCamera.transform.right * inputVector.x;
        movementDirection.y = 0;
        //Rotate into direction
        if (movementDirection.magnitude != 0)
        {            
            playerRotate(movementDirection);
        }
       

        /*
        if (movementDirection.magnitude == 0)
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
        */
    }    

    public void playerRotate(Vector3 targetDirection)
    {
        Quaternion rotationToDo = Quaternion.FromToRotation(transform.forward, targetDirection);
        if(rotationToDo != Quaternion.identity)
        {
            Quaternion.Lerp(transform.rotation, rotationToDo, Time.deltaTime * rotationSpeed); 
        }
            
    }

    public void setAnimation()
    {
        animator.SetFloat("zSpeed", inputManager.movementVector.y * currentSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("xSpeed", inputManager.movementVector.x * currentSpeed, 0.1f, Time.deltaTime);
    }

    //Input Functions
    public void playerJump()
    {
        if (isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravity);
            jumpDirection = Vector3.forward * inputVector.y + Vector3.right * inputVector.x;
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
