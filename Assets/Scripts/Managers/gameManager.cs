using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject UIScreen,startScreen, endScreen;
    private CharacterInput characterInput;
    private bool gameEnded = false, gameStart = false;
    public AnimationClip deathAnimation;
    public TextMeshProUGUI timeText;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterInput = new CharacterInput();
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        UIScreen.SetActive(true);
        Time.timeScale = 1f;
        gameStart = true;
    }   

    public IEnumerator EndGame(float timer)
    {
        characterInput.Character.Move.Disable();
        yield return new WaitForSeconds(deathAnimation.length);
        Time.timeScale = 0f;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        UIScreen.SetActive(false);
        endScreen.SetActive(true);
        timeText.text = "You survived:\r\n" + string.Format("{0:00}:{1:00}", minutes, seconds);
        characterInput.Character.Move.Enable();
        gameEnded = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if(!gameStart && characterInput.Character.Move.WasPerformedThisFrame())
        {
            StartGame();
        }
        if(gameEnded && characterInput.Character.Move.WasPerformedThisFrame())
        {
            RestartGame();
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

}
