using System.Collections;
using System.Collections.Generic;
using Tiles;
using UI.Managers;
using UnityEngine;

public class Stones : MonoBehaviour {

    [SerializeField] private Grid _grid;

    [SerializeField] private Transform[] _blockedPositions;

    private void OnEnable()
    {
        LevelSelectManager.OnGameStarted += SetBlockedTiles;
    }
    
    private void SetBlockedTiles()
    {
        Node currentNode;
        for (int i = 0; i < _blockedPositions.Length; i++)
        {
            currentNode = _grid.GetNodeFromWorldPos(_grid.FindNearestPosition(_blockedPositions[i].position));

            currentNode.SetTileType(TileType.Blocked);
        }
    }

    private void OnDisable()
    {
        LevelSelectManager.OnGameStarted -= SetBlockedTiles;
    }
}
