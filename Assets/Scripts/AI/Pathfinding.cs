using Environment;
using Grid;
using Player;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace PathFinding
{
    public class Pathfinding : MonoBehaviour
    {
        [SerializeField] private TileGrid _grid;
        private List<Node> _path;

        private Dictionary<TileType, Dictionary<Vector2, List<TileType>>> _availableTilesForNode = new Dictionary<TileType, Dictionary<Vector2, List<TileType>>>();

        public List<Node> GetPath()
        {
            return _path;
        }

        private void OnEnable()
        {
            Lava.OnLavaEngulfs += ClearPath;
            PlayerMovement.OnPlayerVictory += ClearPath;
        }

        private void Start()
        {
            AddAvailableTilesForNode();
        }

        private void AddAvailableTilesForNode()
        {
            _path = new List<Node>();

            List<TileType> topNeighbour = new List<TileType>() { TileType.Vertical, TileType.BottomLeft, TileType.BottomRight };
            List<TileType> leftNeighbour = new List<TileType>() { TileType.BottomRight, TileType.Horizontal, TileType.TopRight };
            List<TileType> rightNeighbour = new List<TileType>() { TileType.BottomLeft, TileType.Horizontal, TileType.TopLeft };
            List<TileType> bottomNeighbour = new List<TileType>() { TileType.Vertical, TileType.TopLeft, TileType.TopRight };

            //Bottom left tile
            Dictionary<Vector2, List<TileType>> bottomLeftDic = new Dictionary<Vector2, List<TileType>>
        {
            { new Vector2(0, -1), bottomNeighbour },
            { new Vector2(-1, 0), leftNeighbour }
        };

            _availableTilesForNode.Add(TileType.BottomLeft, bottomLeftDic);


            //Bottom right tile
            Dictionary<Vector2, List<TileType>> bottomRightDic = new Dictionary<Vector2, List<TileType>>
        {
            { new Vector2(0, -1), bottomNeighbour },
            { new Vector2(1, 0), rightNeighbour }
        };

            _availableTilesForNode.Add(TileType.BottomRight, bottomRightDic);

            //Top left tile
            Dictionary<Vector2, List<TileType>> topLeftDic = new Dictionary<Vector2, List<TileType>>
        {
            { new Vector2(0, 1), topNeighbour },
            { new Vector2(-1, 0), leftNeighbour }
        };

            _availableTilesForNode.Add(TileType.TopLeft, topLeftDic);

            //Top right tile
            Dictionary<Vector2, List<TileType>> topRightDic = new Dictionary<Vector2, List<TileType>>
        {
            { new Vector2(0, 1), topNeighbour },
            { new Vector2(1, 0), rightNeighbour }
        };

            _availableTilesForNode.Add(TileType.TopRight, topRightDic);

            //Horizontal tile
            Dictionary<Vector2, List<TileType>> horizontalDic = new Dictionary<Vector2, List<TileType>>
        {
            { new Vector2(-1, 0), leftNeighbour },
            { new Vector2(1, 0), rightNeighbour }
        };

            _availableTilesForNode.Add(TileType.Horizontal, horizontalDic);

            //Vertical tile
            Dictionary<Vector2, List<TileType>> verticalDic = new Dictionary<Vector2, List<TileType>>
        {
            { new Vector2(0, 1), topNeighbour },
            { new Vector2(0, -1), bottomNeighbour }
        };

            _availableTilesForNode.Add(TileType.Vertical, verticalDic);
        }


        public void CalculatePath(Node nextNode, System.Action callback)
        {
            if (_path.Count > 0)
            {
                Node currentNode = _path[_path.Count - 1];

                Vector2 neighbour = nextNode.GetGridPos() - currentNode.GetGridPos();
                TileType nextType = nextNode.GetTileType();



                TileType currentType = currentNode.GetTileType();

                if (_availableTilesForNode[currentType].ContainsKey(neighbour))
                {
                    if (_availableTilesForNode[currentType][neighbour].Contains(nextType) && nextNode.GetWalkable())
                    {
                        _path.Add(nextNode);
                        callback();

                        return;
                    }
                }
                nextNode.SetTileType(TileType.None);
            }
            else
            {
                _path.Add(nextNode);

                callback();
            }
        }

        private void ClearPath()
        {
            _path.Clear();
        }

        private void OnDisable()
        {
            Lava.OnLavaEngulfs -= ClearPath;
            PlayerMovement.OnPlayerVictory -= ClearPath;
        }
    }
}