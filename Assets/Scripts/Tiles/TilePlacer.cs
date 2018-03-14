﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiles {

    public class TilePlacer : MonoBehaviour {
        [SerializeField] private Grid _grid;

        [SerializeField]private GameObject _currentTile = null;
        //public static Action<GameObject> OnChangeTile;

        private bool _canSpawnTile;

        public void PickTile(GameObject tileToPick)
        {
            _currentTile = tileToPick;
            //if (_canSpawnTile)
            //{
            //    _canSpawnTile = false;
            //    //GameObject newTile = Instantiate(tileToPick);
            //    //if (OnChangeTile != null)
            //    //    OnChangeTile(newTile);
            //}
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

        private void PlaceTile(Vector2 inputPos)
        {
            Vector2 nearestPos = _grid.FindNearestPosition(Camera.main.ScreenToWorldPoint(inputPos));
            nearestPos.y -= 1f;
            GameObject tileClone = Instantiate(_currentTile);
            tileClone.transform.position = new Vector2(nearestPos.x + 0.5f, nearestPos.y + 0.7f);
        }

        public void ClickedButtonCheck()
        {
            _canSpawnTile = false;
            StartCoroutine(SpawnTileDelay());
        }

        private IEnumerator SpawnTileDelay()
        {
            yield return new WaitForEndOfFrame();
            _canSpawnTile = true;
        }
    }
}