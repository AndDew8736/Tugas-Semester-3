using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private float timeDelay;
    public void PlayGame(string sceneName)
    {
        StartCoroutine(delayTransition(sceneName, timeDelay));
    }
    IEnumerator delayTransition(string sceneName, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
