using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterInput characterInput;
    public CharacterController controller;
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
    public bool isGrounded = false;
    public float checkGroundedHeight;
    public float rotationSpeed = 3f;
    public LayerMask groundMask;

    private void Awake()
    {
        characterInput = new CharacterInput();
    }

    private void Start()
    {
        //Get Components
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerCamera = Camera.main;
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
        checkGround();
        animator.SetBool("Grounded", isGrounded);

        inputVector = characterInput.Character.Move.ReadValue<Vector2>();
        isRunning = characterInput.Character.Run.IsInProgress();

        //Jump
        if (characterInput.Character.Jump.WasPerformedThisFrame())
        {
            playerJump();

        }

        //Get Direction
        Vector3 movementDirection = playerCamera.transform.forward * inputVector.y + playerCamera.transform.right * inputVector.x;
        movementDirection.y = 0;
        //Rotate into direction
        if (movementDirection.magnitude != 0)
        {            
            playerRotate(movementDirection);
        }
       
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
            controller.Move(jumpDirection * currentSpeed * Time.deltaTime);
        }
        //Vertical
        if (isGrounded && playerVelocity.y <= 0f)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y -= gravity*Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }    

    public void playerRotate(Vector3 targetDirection)
    {
        float angleToRotate = Quaternion.FromToRotation(transform.forward, targetDirection).eulerAngles.y;
        if(angleToRotate > 180)
        {
            angleToRotate -= 360f;
        }
        transform.Rotate(Vector3.up, angleToRotate * Time.deltaTime * rotationSpeed);                 
    }

    public void setAnimation()
    {
        animator.SetFloat("zSpeed", inputVector.y * currentSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("xSpeed", inputVector.x * currentSpeed, 0.1f, Time.deltaTime);
    }
    
    private void checkGround()
    {
       isGrounded = Physics.CheckBox(transform.position + Vector3.up * checkGroundedHeight,new Vector3(controller.radius,0.1f, controller.radius),Quaternion.identity,groundMask);
    }
    public void playerJump()
    {
        if (isGrounded)
        {
            animator.SetTrigger("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravity );
            jumpDirection = playerCamera.transform.forward * inputVector.y + playerCamera.transform.right * inputVector.x;
        }
    }


    private void OnEnable()
    {
        characterInput.Enable();
    }

    private void OnDisable()
    {
        characterInput.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position+ Vector3.up* checkGroundedHeight, new Vector3(controller.radius*2, 0.2f, controller.radius*2));
    }
}
