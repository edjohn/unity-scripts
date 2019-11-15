using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 1f;
    public void LoadGameOver()
    {
        StartCoroutine(GameOver());
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(2);
    }
}
