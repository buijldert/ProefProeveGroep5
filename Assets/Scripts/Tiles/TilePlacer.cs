using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiles {

    public class TilePlacer : MonoBehaviour {
        [SerializeField] private Grid _grid;

        private int _currentLayer = 0;

        [SerializeField]private GameObject _currentTile = null;

        [SerializeField] private Transform parent;

        //public static Action<GameObject> OnChangeTile;

        private bool _canSpawnTile;

        public void PickTile(GameObject tileToPick)
        {
            _currentTile = tileToPick;
        }

        private void Update()
        {
            if(_canSpawnTile)
            {
                if (Input.touchCount > 0)
                {
                    PlaceTile(Input.GetTouch(0).position);
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    PlaceTile(Input.mousePosition);
                }
            }
        }

        /// <summary>
        /// Places a tile at the input position in worldspace.
        /// </summary>
        /// <param name="inputPos">The input position.</param>
        private void PlaceTile(Vector2 inputPos)
        {
            
            Vector2 nearestPos = _grid.FindNearestPosition(Camera.main.ScreenToWorldPoint(inputPos));
            if(nearestPos != Vector2.zero)
            {
                nearestPos.y -= 1f;
                GameObject tileClone = Instantiate(_currentTile);
                tileClone.transform.SetParent(parent);

                tileClone.transform.position = new Vector2(nearestPos.x + 0.5f, nearestPos.y + 0.7f);
                tileClone.GetComponent<SpriteRenderer>().sortingOrder = _currentLayer;
                _currentLayer += 1;
            }
        }

        /// <summary>
        /// Makes sure input and button clicking dont overlap.
        /// </summary>
        public void ClickedButtonCheck()
        {
            _canSpawnTile = false;
            StartCoroutine(SpawnTileDelay());
        }

        /// <summary>
        /// Adds a delay before tiles are placeable.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnTileDelay()
        {
            yield return new WaitForEndOfFrame();
            _canSpawnTile = true;
        }
    }
}