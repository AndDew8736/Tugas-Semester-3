using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currenyUI;
    [SerializeField] TextMeshProUGUI playerHealth;
    [SerializeField] Animator anim;
    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("Menu Open", isMenuOpen);
    }
    private void OnGUI()
    {
        currenyUI.text = LevelManager.main.money.ToString();
        playerHealth.text = LevelManager.main.playerHealth.ToString();
    }
}
