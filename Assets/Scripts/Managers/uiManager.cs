using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    private CharacterInput characterInput;
    private bool isPaused = false;
    public GameObject gameUI;
    public GameObject pauseUI;
    public Toggle xToggle, yToggle;
    public Slider slider;
    public CinemachineFreeLook cmCamera;
    public Settings settingsObject;

    private void Awake()
    {
        characterInput = new CharacterInput();
    }

    private void Start()
    {
        InvertCameraAxisX(settingsObject.xAxisInverted);
        InvertCameraAxisY(settingsObject.yAxisInverted);
        SetCameraAcceleration(settingsObject.sensitivity);
    }

    private void Update()
    {
        if (characterInput.Character.Pause.WasPerformedThisFrame())
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Continue();
            }
        }
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        gameUI.SetActive(false);
        pauseUI.SetActive(true);
        xToggle.isOn = settingsObject.xAxisInverted;
        yToggle.isOn = settingsObject.yAxisInverted;
        slider.value = settingsObject.sensitivity;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
        pauseUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void InvertCameraAxisX(bool uiInput)
    {
       cmCamera.m_XAxis.m_InvertInput = uiInput;
       settingsObject.xAxisInverted = uiInput;
    }
    public void InvertCameraAxisY(bool uiInput)
    {
       cmCamera.m_YAxis.m_InvertInput = uiInput;
        settingsObject.yAxisInverted = uiInput;
    }

    public void SetCameraAcceleration(float acceleration)
    {
        cmCamera.m_XAxis.m_AccelTime = cmCamera.m_YAxis.m_AccelTime = acceleration;
        settingsObject.sensitivity = acceleration;
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
