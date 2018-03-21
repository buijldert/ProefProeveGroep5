﻿using Tiles;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class UITile : MonoBehaviour 
{
    [SerializeField] private TileType _tileType;

    private void OnEnable()
    {
        UIController.OnThemeChanged += OnThemeChanged;
        UIController.instance.UpdateTheme();
    }

    private void OnThemeChanged(UITheme UITheme)
    {
        Sprite temp = null;

        switch (_tileType)
        {
            case TileType.Horizontal:
                temp = UITheme.Horizontal;
                break;
            case TileType.Vertical:
                temp = UITheme.Vertical;
                break;
            case TileType.TopLeft:
                temp = UITheme.CornerLeftUp;
                break;
            case TileType.BottomLeft:
                temp = UITheme.CornerLeft;
                break;
            case TileType.TopRight:
                temp = UITheme.CornerRightUp;
                break;
            case TileType.BottomRight:
                temp = UITheme.CornerRight;
                break;
        }

        GetComponent<Image>().sprite = temp;
    }
	
    private void OnDisable()
    {
        UIController.OnThemeChanged -= OnThemeChanged;
    }
}