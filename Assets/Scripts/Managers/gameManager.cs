using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject UIScreen, endScreen;
    private CharacterInput characterInput;
    private bool gameEnded = false;
    public AnimationClip deathAnimation;

    private void Awake()
    {
        characterInput = new CharacterInput();
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(deathAnimation.length);
        Time.timeScale = 0f;
        UIScreen.SetActive(false);
        endScreen.SetActive(true);
        gameEnded = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
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
