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
    public Slider slider;
    public CinemachineFreeLook cmCamera;

    private void Awake()
    {
        characterInput = new CharacterInput();
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
    }
    public void Continue()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void InvertCameraAxisX(bool uiInput)
    {
        Debug.Log(uiInput);
       cmCamera.m_XAxis.m_InvertInput = uiInput;
    }
    public void InvertCameraAxisY(bool uiInput)
    {
        Debug.Log(uiInput);
       cmCamera.m_YAxis.m_InvertInput = uiInput;
    }

    public void SetCameraAcceleration(float acceleration)
    {
        cmCamera.m_XAxis.m_AccelTime = cmCamera.m_YAxis.m_AccelTime = acceleration;
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
