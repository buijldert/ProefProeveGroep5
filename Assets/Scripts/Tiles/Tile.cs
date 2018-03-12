using UnityEngine;

public enum TileType {
    Horizontal,
    Vertical,
    TopLeft,
    BottomLeft,
    TopRight,
    BottomRight
}

public class Tile : MonoBehaviour {
    [SerializeField] private TileType _tileType;
}
