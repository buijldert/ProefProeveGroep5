using UnityEngine;
using UI.Controllers;
using UI.ScriptableObjects;

namespace Tiles {
    /// <summary>
    /// An enum for use when setting the tiletypes.
    /// </summary>
    public enum TileType {
        Horizontal,
        Vertical,
        TopLeft,
        BottomLeft,
        TopRight,
        BottomRight,
        None
    }

    public class Tile : MonoBehaviour {
        [SerializeField] private TileType _tileType;

        private void OnEnable() {
            UIController.OnThemeChanged += OnThemeChanged;
            UIController.instance.UpdateTheme();
        }

        /// <summary>
        /// Returns the tiletype of this tile.
        /// </summary>
        /// <returns>The tile type.</returns>
        public TileType GetTileType() {
            return _tileType;
        }

        /// <summary>
        /// Changes the tile theme.
        /// </summary>
        /// <param name="UITheme"></param>
        private void OnThemeChanged(UITheme UITheme) {
            Sprite temp = null;

            switch (_tileType) {
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

            GetComponent<SpriteRenderer>().sprite = temp;
        }

        private void OnDisable() {
            UIController.OnThemeChanged -= OnThemeChanged;
        }
    }
}