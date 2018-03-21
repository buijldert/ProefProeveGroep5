using UnityEngine;

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

        public TileType GetTileType()
        {
            return _tileType;
        }
    }
}