using System.Collections;
using UnityEngine;

namespace Tiles {

    public class TestTileGrid : MonoBehaviour {

        private Vector2[,] _tilePositions = new Vector2[6, 9];

        private float _x, _y;
        private float _offsetY;

        [SerializeField] private Camera _mainCamera;

        [SerializeField] private GameObject gridItem;

        private void OnEnable()
        {
            DragTile.OnGetGridPos += FindNearestPosition;
        }

        private void Start()
        {
            StartCoroutine(SpawnGridDelay());
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

        private IEnumerator SpawnGridDelay()
        {
            _x = (Screen.width / _tilePositions.GetLength(0)) / 2;
            _y = (Screen.height / 11) / 2;

            float xCalc = _x * 2;
            float yCalc = _y * 2;

            _offsetY = _y * 3;
            Vector2 newPos;
            for (int i = 0; i < _tilePositions.GetLength(1); i++)
            {
                for (int j = 0; j < _tilePositions.GetLength(0); j++)
                {
                    newPos = _mainCamera.ScreenToWorldPoint(new Vector2(_x, _y + _offsetY));
                    //Instantiate(gridItem, newPos, Quaternion.identity);
                    _tilePositions[j, i] = newPos;
                    _x += xCalc;
                    yield return new WaitForEndOfFrame();
                }
                _x = xCalc / 2;
                _y += yCalc;
            }
        }

        private void OnDisable()
        {
            DragTile.OnGetGridPos -= FindNearestPosition;
        }
    }
}