using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Death : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI roundCounter;
    public static UnityEvent Dies = new UnityEvent();
    private void Awake()
    {
        Dies.AddListener(EndScreen);
    }
    private void EndScreen()
    {
        //open end ui and run the animation for it
        roundCounter.text = "Rounds Survived : " + EnemySpawner.main.currentWave;
        endScreen.SetActive(true);
    }
    public void Restart(string currentLevel)
    {
        SceneManager.LoadSceneAsync(currentLevel);
    }
    public void Exit()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
