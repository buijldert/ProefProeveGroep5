using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiles {

    public class TestTileGrid : MonoBehaviour {

        private Vector2[,] _tilePositions = new Vector2[9, 14];

        private float _x, _y;
        private float _offsetY;

        [SerializeField] private Camera _mainCamera;

        private void OnEnable()
        {
            DragTile.OnGetGridPos += FindNearestPosition;
        }

        private void Start()
        {
            _x = (Screen.width / _tilePositions.GetLength(0)) / 2;
            _y = (Screen.height / 16) / 2;

            _offsetY = _y * 4;
            Vector2 newPos;
            for (int i = 0; i < _tilePositions.GetLength(1); i++)
            {
                for (int j = 0; j < _tilePositions.GetLength(0); j++)
                {
                    newPos = _mainCamera.ScreenToWorldPoint(new Vector2(_x, _y + _offsetY));
                    _tilePositions[j, i] = newPos;
                    _x += Screen.width / _tilePositions.GetLength(0);
                }
                _x = (Screen.width / _tilePositions.GetLength(0)) / 2;
                _y += Screen.height / 16;
            }
        }

        public Vector2 FindNearestPosition(Vector2 dropPos)
        {
            float lowestDistance = 1000.0f;
            Vector2 nearestPos = Vector2.zero;
            for (int i = 0; i < _tilePositions.GetLength(1); i++)
            {
                for (int j = 0; j < _tilePositions.GetLength(0); j++)
                {
                    if(Vector2.Distance(dropPos, _tilePositions[j, i]) < lowestDistance)
                    {
                        lowestDistance = Vector2.Distance(dropPos, _tilePositions[j, i]);
                        nearestPos = _tilePositions[j, i];
                    }
                }
            }

            return nearestPos;
        }

        private void OnDisable()
        {
            DragTile.OnGetGridPos -= FindNearestPosition;
        }
    }
}