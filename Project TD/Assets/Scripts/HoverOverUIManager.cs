using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
public static UIManager main;
    private bool HoveringOverUI;
    private void Awake()
    {
        main = this;
    }
    public void SetHoverState(bool state)
    {
        HoveringOverUI = state;
    }
    public bool isHoveringUI()
    {
        return HoveringOverUI;
    }
}
