
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header("Refferences")]
    [SerializeField] private TowerBuilder[] towerPrefabs;
    private int selectedTower = 0;
    public static BuildManager main;

    private void Awake()
    {
        main = this;
    }
    public TowerBuilder GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    }
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
