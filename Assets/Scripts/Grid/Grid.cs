using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
	[SerializeField]private int _gridWidth;
	[SerializeField]private int _gridHeight;

	[SerializeField]private float _nodeDiameter = 1;

	private Node[,] _grid;
	private float _nodeRadius;

	private void Start() {
		_grid = new Node[_gridWidth, _gridHeight];

		_nodeRadius = _nodeDiameter / 2f;

		CreateGrid ();
	}

	private void CreateGrid() {
		for (int x = 0; x < _gridWidth; x++) {
			for (int y = 0; y < _gridHeight; y++) {
				Vector2 gridPos = new Vector2 (x * _nodeDiameter, y * _nodeDiameter);
				Vector2 worldPos = GridToWorldPos (gridPos);

				Node node = new Node (gridPos, worldPos);

				_grid [x, y] = node;
			}
		}
	}

	public Vector2 GridToWorldPos(Vector2 gridPos) {
		Vector2 worldPos = new Vector2 ();

		worldPos.x = gridPos.x - _gridWidth / 2f + _nodeRadius;
		worldPos.y = gridPos.y - _gridHeight / 2f + _nodeRadius;

		return worldPos;
	}

	public Vector2 WorldToGridPos(Vector2 worldPos) {
		Vector2 gridPos = new Vector2 ();

		gridPos.x = worldPos.x + (_gridWidth / 2f) - _nodeRadius;
		gridPos.y = worldPos.y + (_gridHeight / 2f) - _nodeRadius;
		
		return gridPos;
	}

	public Node GetNodeFromWorldPos(Vector2 worldPos) {
		Vector2 gridPos = WorldToGridPos (worldPos);

		Vector2 arrayGridPos = new Vector2 (gridPos.x / _nodeDiameter, gridPos.y / _nodeDiameter);

		return _grid[(int)arrayGridPos.x, (int)arrayGridPos.y];
	}

	public List<Node> GetNeighbours(Node node) {
		return new List<Node> ();
	}

	private void OnDrawGizmos() {
		if (_grid != null) {
			Gizmos.color = Color.green;
			for (int x = 0; x < _gridWidth; x++) {
				for (int y = 0; y < _gridHeight; y++) {
					Gizmos.DrawWireCube (_grid [x, y].GetWorldPos (), new Vector2(_nodeDiameter, _nodeDiameter));
				}
			}
		}
	}
}
