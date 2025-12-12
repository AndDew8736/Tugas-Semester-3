using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private Color hoverColor;
    public GameObject towerObj;
    public TowerController tower;
    private Color startColor;

    private void Start()
    {
        startColor = tileSprite.color;
    }
    private void OnMouseEnter()
    {
        tileSprite.color = hoverColor;
        //on mouse hover
    }
    private void OnMouseExit()
    {
        tileSprite.color = startColor;
    }
    private void OnMouseDown()
    {
        //on mouse click
        if(UIManager.main.isHoveringUI()) return;
        if (towerObj != null)
        {
            tower.openUpgrade();
            return;
        }

        TowerBuilder towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild.cost > LevelManager.main.money)
        {
            //broke lmao  
            return;
        }

        LevelManager.main.spendMoney(towerToBuild.cost);
        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        tower = towerObj.GetComponent<TowerController>();
        tower.setTile(this);
        //connects the tower and the tile its on
    }
    public void towerDelete()
    {
        towerObj = null;
        tower = null;
    }
}
