using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    private CharacterInput characterInput;
    private Animator animator;
    private Camera playerCamera;
    private AudioSource playerAudioSource;
    public GameObject ovalObject;
    public AudioSource screechAudioSource;

    [Header("Movement Variables")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpHeight = 5f;
    public bool isGrounded = false;
    public float checkGroundedHeight;
    public float rotationSpeed = 3f;
    public LayerMask groundMask;
    public float outsideDamageTime = 1;
    public float outsideDamage = 2;

    private Vector2 inputVector = Vector2.zero;
    private Vector3 playerVelocity = Vector3.zero;
    private float currentSpeed = 0f;
    private bool isRunning;
    private Vector3 jumpDirection = Vector3.zero;
    private float gravity = 9.81f;
    private bool isOutside = false;
    private Vector3 outsidePosition;
    private float outsideTimer;
    private int outsideDamageCount = 1;

    [Header("AudioClips")]
    public AudioClip runningClip;
    public AudioClip walkingClip;

    private void Awake()
    {
        characterInput = new CharacterInput();
    }

    private void Start()
    {
        //Get Components
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
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

        //UI Oval
        if (isOutside)
        {
            outsideTimer -= Time.deltaTime;
            if(outsideTimer <= 0f)
            {
                FindFirstObjectByType<healthManager>().DecreaseHealth(outsideDamageCount * outsideDamage);
                outsideDamageCount++;
                outsideTimer = outsideDamageTime;
            }
            float distance = (transform.position - outsidePosition).magnitude;
            adjustOvalAlpha(Mathf.InverseLerp(0, 50f, distance));
        }

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
                setAudioClip(runningClip);
            }
            else
            {
                currentSpeed = walkSpeed;
                setAudioClip(walkingClip);
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
        //Audio
        if (movementDirection.magnitude != 0 && isGrounded && !playerAudioSource.isPlaying)
        {
            PlayAudio();
        }
        else if (movementDirection.magnitude == 0 || !isGrounded)
        {
            StopAudio();
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

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("GameOver");
        screechAudioSource.Stop();
    }

    private void setAudioClip(AudioClip audioClip)
    {
        playerAudioSource.clip = audioClip;
    }

    private void PlayAudio()
    {
        playerAudioSource.Play();
    }

    private void StopAudio()
    {
        playerAudioSource.Stop();
    }

    private void adjustOvalAlpha(float alpha)
    {
        Color newColor = ovalObject.GetComponent<Image>().color;
        newColor.a = alpha;
        ovalObject.GetComponent<Image>().color = newColor;
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

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Limit")
        {
            screechAudioSource.Play();
            outsidePosition = transform.position;
            outsideTimer = outsideDamageTime;
            isOutside = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Limit")
        {
            screechAudioSource.Stop();
            adjustOvalAlpha(0f);
            isOutside = false;
            outsideDamageCount = 0;
        }
    }
}
