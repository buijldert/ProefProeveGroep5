﻿using Environment;
using Player;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace Grid
{
    public class TileGrid : MonoBehaviour
    {
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;

        private int _nodeDiameter = 1;

        private Node[,] _grid;
        private float _nodeRadius;

        private void Awake()
        {
            _grid = new Node[_gridWidth, _gridHeight];

            _nodeRadius = _nodeDiameter / 2f;

            CreateGrid();
        }

        private void OnEnable()
        {
            Lava.OnLavaEngulfs += ResetGrid;
            PlayerMovement.OnPlayerVictory += ResetGrid;
        }

        private void CreateGrid()
        {

            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    Vector2 gridPos = new Vector2(x, y);
                    Vector2 worldPos = GridToWorldPos(new Vector2(gridPos.x * _nodeDiameter, gridPos.y * _nodeDiameter));

                    RaycastHit hit = new RaycastHit();
                    bool walkable = true;

                    if (Physics.Raycast(new Vector3(worldPos.x, worldPos.y, -1), new Vector3(0, 0, 1), out hit, 3f))
                    {
                        if (hit.collider != null)
                        {
                            walkable = false;
                        }
                    }

                    Node node = new Node(gridPos, worldPos, walkable);


                    _grid[x, y] = node;
                }
            }
        }

        private void ResetGrid()
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    _grid[x, y].SetTileType(TileType.None);
                }
            }
        }

        public Vector2 GridToWorldPos(Vector2 gridPos)
        {
            Vector2 worldPos = new Vector2
            {
                x = (gridPos.x - (_gridWidth / 2f) + _nodeRadius) + transform.position.x,
                y = (gridPos.y - (_gridHeight / 2f) + _nodeRadius) + transform.position.y
            };

            return worldPos;
        }

        public Vector2 WorldToGridPos(Vector2 worldPos)
        {
            Vector2 gridPos = new Vector2
            {
                x = (worldPos.x + (_gridWidth / 2f) - _nodeRadius) - transform.position.x,
                y = (worldPos.y + (_gridHeight / 2f) + _nodeRadius) - transform.position.y
            };

            return new Vector2(gridPos.x / _nodeDiameter, gridPos.y / _nodeDiameter);
        }


        public Node GetNodeFromWorldPos(Vector2 worldPos)
        {
            Vector2 gridPos = WorldToGridPos(worldPos);


            Vector2 arrayGridPos = new Vector2(gridPos.x / _nodeDiameter, gridPos.y / _nodeDiameter);
            Node node = _grid[(int)arrayGridPos.x, (int)arrayGridPos.y];

            return node;
        }

        public Vector2 FindNearestPosition(Vector2 worldPos)
        {
            Vector2 nearestPos = new Vector2();

            nearestPos = new Vector2(Mathf.Ceil(worldPos.x) - _nodeRadius, Mathf.Ceil(worldPos.y) - _nodeRadius);

            Vector2 minimumPosition = _grid[0, 0].GetWorldPos();
            Vector2 maximumPosition = _grid[_gridWidth - 1, _gridHeight - 1].GetWorldPos();

            if (nearestPos.x >= minimumPosition.x - _nodeRadius && nearestPos.x <= maximumPosition.x + _nodeRadius)
            {
                if (nearestPos.y >= minimumPosition.y - _nodeRadius && nearestPos.y <= maximumPosition.y + _nodeRadius)
                {
                    return nearestPos;
                }
            }
            return Vector2.zero;
        }

        public List<Vector2> GetNeighbours(Node node)
        {
            List<Vector2> neighbours = new List<Vector2>();

            for (int x = -1; x <= 1; x += 2)
            {
                for (int y = -1; y <= 1; y += 2)
                {
                    Vector2 gridPos = node.GetGridPos();
                    if (gridPos.x + x >= 0 && gridPos.x + x < _gridWidth &&
                           gridPos.y + y >= 0 && gridPos.y + y < _gridHeight)
                    {
                        neighbours.Add(new Vector2(x, y));
                    }
                }
            }

            return neighbours;
        }

        private void OnDrawGizmos()
        {
            if (_grid != null)
            {

                for (int x = 0; x < _gridWidth; x++)
                {
                    for (int y = 0; y < _gridHeight; y++)
                    {
                        if (_grid[x, y].GetWalkable())
                        {
                            Gizmos.color = Color.green;
                        }
                        else
                        {
                            Gizmos.color = Color.red;
                        }
                        Gizmos.DrawWireCube(_grid[x, y].GetWorldPos(), new Vector3(_nodeDiameter, _nodeDiameter, 1));
                    }
                }
            }
        }

        private void OnDisable()
        {
            Lava.OnLavaEngulfs -= ResetGrid;
            PlayerMovement.OnPlayerVictory -= ResetGrid;
        }
    }
}