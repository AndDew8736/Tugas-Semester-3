using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;
using System;
using Unity.Mathematics;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TowerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject TowerSprite;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUi;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;    
    [SerializeField] private TileManager currentTile;

    [Header("Stats")]
    [SerializeField] public int dmg = 1;
    [SerializeField] public float shotsPerSecond = 1f;
    [SerializeField] public float targetingRange = 8f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] public int UpgradeCost = 100;
    private int baseUpgradeCost;
    private float baseShotsPerSecond;
    private float baseTargetingRange;
    private Transform target;
    private float timeUntilShot;
    private int level = 1;
    public UpgradeUIManager uiScript;
    private void Start()
    {
        baseUpgradeCost = UpgradeCost;
        baseShotsPerSecond = shotsPerSecond;
        baseTargetingRange = targetingRange;
        upgradeButton.onClick.AddListener(Upgrade); 
        sellButton.onClick.AddListener(Sell);

        uiScript = upgradeUi.GetComponent<UpgradeUIManager>();
        uiScript.setTower(this);
    }
    public void setTile(TileManager tile)
    {
        currentTile = tile;   
    }
    // Update is called once per frame
    private void Update()
    {
        timeUntilShot += Time.deltaTime;
        if (target == null)
        {
            findTarget();
            return;
        }
        else
        {
            if (timeUntilShot >= 1f / shotsPerSecond)
            {
                shoot();
                timeUntilShot = 0f;
            }
        }
        rotateToTarget();
        if (!checkInRange())
        {
            target = null;
        }
    }
    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
        (Vector2)transform.position, 0f, enemyMask);
        //does a raycast to determine the target
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
        //if the raycast hits a n enemy, sets it as the target
    }
    private void rotateToTarget()
    {
        float angle = Mathf.Atan2(target.position.y - TowerSprite.transform.position.y, target.position.x - TowerSprite.transform.position.x)
        * Mathf.Rad2Deg;
        //gets the angle by getting the degree and then converting it to rad
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        TowerSprite.transform.rotation = Quaternion.RotateTowards(TowerSprite.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //quaternion to make targetrotation with the vectors of 0,0, angle
        //makes the transform rotation snap to a target slowly instead of instantly
    }
    private bool checkInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    private void shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, TowerSprite.transform.rotation);
        ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
        projectileScript.setTarget(target);
        projectileScript.setDmg(dmg);
        //spawns in a gameobject projectile with firing position and quaternion for the angle
        //takes parts of the projectile controller script
        //sets the target of the tower to the target of the projectile controller
    }   
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
        //makes the preview range circle
    }
    private void Upgrade()
    {
        if(baseUpgradeCost > LevelManager.main.money)
        return;
        UpgradeCost = calculateCost();
        LevelManager.main.spendMoney(UpgradeCost);
        level++;
        dmg += 1;
        shotsPerSecond = calculateShotsPerSecond();
        targetingRange = calculateRange();

        closeUpgrade();
    }
    private int calculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level,0.8f));
    }
    private float calculateShotsPerSecond()
    {
        return Mathf.RoundToInt(baseShotsPerSecond * Mathf.Pow(level,0.8f)) ;
    } 
    private float calculateRange()
    {
        return Mathf.RoundToInt(baseTargetingRange * Mathf.Pow(level,0.4f));
    }
    public void openUpgrade()
    {
        upgradeUi.SetActive(true);
    }
   
    public void closeUpgrade()
    {
        upgradeUi.SetActive(false);
        UIManager.main.SetHoverState(false);
    }
    private void Sell()
    {
        LevelManager.main.moneyUp(UpgradeCost);
        closeUpgrade();
        currentTile.towerDelete();
        Destroy(gameObject);
    }
}
