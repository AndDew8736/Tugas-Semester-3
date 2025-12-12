using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{    
    public static EnemyHealth main;

    [Header("Stats")]
    [SerializeField] public int HP = 2;
    [SerializeField] private int moneyDrop = 5;
    private bool isDestroyed = false;
    public void takeDMG(int dmg)
    {
        HP -= dmg;
        if (HP <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            isDestroyed = true;
            LevelManager.main.moneyUp(moneyDrop);
            Destroy(gameObject);
        }
    }
}
