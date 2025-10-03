using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private Color hoverColor;
    private GameObject tower;
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
        if (tower != null)
        {
            return;
        }
           
        GameObject towerToBuild = BuildManager.main.GetSelectedTower();
        LevelManager.main.spendMoney(50);
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);

    }
}
