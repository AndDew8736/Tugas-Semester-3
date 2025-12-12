using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform[] path;
    public Transform startPoint;
    public int playerHealth = 100;
    public int money;
    public int researchPoints = 0;
    private void Awake()
    {
        main = this;
    }
    public void moneyUp(int amount)
    {
        money += amount;
    }
    public bool spendMoney(int amount)
    {
        if (amount <= money)
        {
            //buy
            money -= amount;
            return true;
        }
        else
        {
            //broke
            return false;
        }
    }
    public void playerTakesDamage(int dmg)
    {
        playerHealth -= dmg;
        if(playerHealth <= 0)
        {
            //Into the inferno
            Death.Dies.Invoke();
        }
    }
}
