using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI towerATK;
    [SerializeField] TextMeshProUGUI towerSPD;
    [SerializeField] TextMeshProUGUI towerRANGE;
    [SerializeField] TextMeshProUGUI upgradeCost;
    [SerializeField] TowerController currTower;
    public void setTower(TowerController tower)
    {
        currTower = tower;
    }
    public void OnGUI()
    {
        towerATK.text = ": " + currTower.dmg.ToString();
        towerSPD.text = ": " + currTower.shotsPerSecond.ToString();
        towerRANGE.text = ": " + currTower.targetingRange.ToString();
        upgradeCost.text = "$" + currTower.UpgradeCost.ToString();
    }
    public bool mouse_over = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        UIManager.main.SetHoverState(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        UIManager.main.SetHoverState(false);
        gameObject.SetActive(false);
        //set active false hides the ui
    }
}
