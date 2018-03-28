using Environment;
using Grid;
using PathFinding;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiles {

    public class TilePlacer : MonoBehaviour {
        public static Action OnLavaStart;

        [SerializeField] private TileGrid _grid;
		[SerializeField] private Pathfinding _finding;
        
        private GameObject _currentTile;

		private Vector2 _currentPosition;

		private Node _currentNode;

        private bool _canSpawnTile;

        private int _numberOfTilesPlaced;

        private void OnEnable() {
            Lava.OnLavaEngulfs += RemoveTiles;
            PlayerMovement.OnPlayerVictory += RemoveTiles;
        }

        /// <summary>
        /// Picks a tile with a button or event trigger.
        /// </summary>
        /// <param name="tileToPick">The tile to be picked.</param>
        public void PickTile(GameObject tileToPick) {
            _currentTile = tileToPick;
        }
        
        private void Update() {
            if(_canSpawnTile) {
                TilePlacingInput();
            }
        }

        private void TilePlacingInput()
        {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    PlaceTile(Input.GetTouch(0).position);
            }
            else if (Input.GetMouseButtonDown(0)) {
                PlaceTile(Input.mousePosition);
            }
        }

        /// <summary>
        /// Places the tile at the input position.
        /// </summary>
        /// <param name="inputPos">The position of the input(touch/mouse).</param>
        private void PlaceTile(Vector2 inputPos) {
			_currentNode = _grid.GetNodeFromWorldPos (_grid.FindNearestPosition (Camera.main.ScreenToWorldPoint (inputPos)));

            if(_currentNode.GetTileType() == TileType.None) {
                _currentNode.SetTileType(_currentTile.GetComponent<Tile>().GetTileType());

                _finding.CalculatePath(_currentNode, PlaceTileCallback);

                _numberOfTilesPlaced++;
                if (_numberOfTilesPlaced == 2)
                    if (OnLavaStart != null)
                        OnLavaStart();
            }
        }

        /// <summary>
        /// A callback to place the tile after a position has been found.
        /// </summary>
		public void PlaceTileCallback() {
			GameObject tileClone = Instantiate(_currentTile);

			tileClone.transform.position = _currentNode.GetWorldPos ();
            tileClone.transform.SetParent(transform);
		}

        /// <summary>
        /// Checks if the button has clicked so inputs dont interfere with each other.
        /// </summary>
        public void ClickedButtonCheck() {
            _canSpawnTile = false;
            StartCoroutine(SpawnTileDelay());
        }

        /// <summary>
        /// Adds a little delay before the player can place a tile.
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnTileDelay() {
            yield return new WaitForEndOfFrame();
            _canSpawnTile = true;
        }

        /// <summary>
        /// Removes all tiles for the reset.
        /// </summary>
        private void RemoveTiles() {
            for (int i = 0; i < transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
            _canSpawnTile = false;
            _numberOfTilesPlaced = 0;
        }

        private void OnDisable() {
            Lava.OnLavaEngulfs -= RemoveTiles;
            PlayerMovement.OnPlayerVictory -= RemoveTiles;
        }
    }
}