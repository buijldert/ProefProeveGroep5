using Tiles;
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

    /// <summary>
    /// Sets the theme for the tile.
    /// </summary>
    /// <param name="UITheme">UITheme.</param>
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
                temp = UITheme.CornerRightUp;
                break;
            case TileType.BottomLeft:
                temp = UITheme.CornerLeft;
                break;
            case TileType.TopRight:
                temp = UITheme.CornerLeftUp;
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
