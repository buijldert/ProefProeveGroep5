using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiles {

    public class TilePlacer : MonoBehaviour {

        public static Action OnLavaStart;

        [SerializeField] private Grid _grid;
		[SerializeField] private Pathfinding _finding;

        [SerializeField]private GameObject _currentTile;

		private Vector2 _currentPosition;

		private Node _currentNode;

        private bool _canSpawnTile;

        private int _numberOfTilesPlaced;

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
                    if(Input.GetTouch(0).phase == TouchPhase.Began)
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
			_currentNode = _grid.GetNodeFromWorldPos (_grid.FindNearestPosition (Camera.main.ScreenToWorldPoint (inputPos)));

			_currentNode.SetTileType (_currentTile.GetComponent<Tile> ().GetTileType ());

			_finding.CalculatePath (_currentNode, PlaceTileCallback);

            _numberOfTilesPlaced++;
            if (_numberOfTilesPlaced == 2)
                if(OnLavaStart != null)
                    OnLavaStart();

        }

		public void PlaceTileCallback() {
			GameObject tileClone = Instantiate(_currentTile);

			tileClone.transform.position = _currentNode.GetWorldPos ();
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