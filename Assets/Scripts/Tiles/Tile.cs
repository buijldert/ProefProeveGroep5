using UnityEngine;
using UI.Controllers;
using System.Diagnostics;

namespace Tiles
{
    public enum TileType
    {
        Horizontal,
        Vertical,
        TopLeft,
        BottomLeft,
        TopRight,
        BottomRight,
        None
    }

    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileType _tileType;

        private void OnEnable()
        {
            UIController.OnThemeChanged += OnThemeChanged;
            UIController.instance.UpdateTheme();
        }

        public TileType GetTileType()
        {
            return _tileType;
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

            GetComponent<SpriteRenderer>().sprite = temp;
        }

        private void OnDisable()
        {
            UIController.OnThemeChanged -= OnThemeChanged;
        }
    }
}