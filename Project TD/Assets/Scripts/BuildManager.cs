using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header("Refferences")]
    [SerializeField] private GameObject[] towerPrefabs;
    private int selectedTower = 0;
    public static BuildManager main;

    private void Awake()
    {
        main = this;
    }
    public GameObject GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    }
}
