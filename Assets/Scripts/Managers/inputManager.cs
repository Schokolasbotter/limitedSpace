using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    [System.Serializable]
    public class vector2Event : UnityEvent<Vector2>{}
    private CharacterInput characterInput;

    public vector2Event OnMove;
    public UnityEvent OnJump;
    public UnityEvent OnRun;

    public Vector2 movementVector;
    public bool isRunning;
    public Vector2 cameraMoveVector;

    private void Awake()
    {
        characterInput = new CharacterInput();
    }

    private void Start()
    {
        if(OnMove == null)
        {
            OnMove = new vector2Event();
        }
    }
    private void Update()
    {
        movementInput();
        jumpInput();
        runInput();
        cameraMoveInput();
        
    }

    private void movementInput()
    {
        movementVector = characterInput.Character.Move.ReadValue<Vector2>();
        
    }

    private void jumpInput()
    {
        if (characterInput.Character.Jump.IsPressed()) { OnJump.Invoke(); }
    }

    private void runInput()
    {
        isRunning = (characterInput.Character.Run.IsInProgress())?true:false;
    }

    private void cameraMoveInput()
    {
        cameraMoveVector = characterInput.Character.CameraMove.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        characterInput.Enable();
    }

    private void OnDisable()
    {
        characterInput.Disable();
    }
}
