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
				Vector2 gridPos = new Vector2 (x, y);
				Vector2 worldPos = GridToWorldPos (new Vector2(gridPos.x * _nodeDiameter, gridPos.y * _nodeDiameter));

				Node node = new Node (gridPos, worldPos);

				Debug.Log (gridPos + " - " + WorldToGridPos (worldPos));

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

		worldPos.x = GetNearestMultiplyOfNodeDiameter (worldPos.x);
		worldPos.y = GetNearestMultiplyOfNodeDiameter (worldPos.y);

		gridPos.x = worldPos.x + (_gridWidth / 2f) - _nodeRadius;
		gridPos.y = worldPos.y + (_gridHeight / 2f) - _nodeRadius;
		
		return new Vector2(gridPos.x / _nodeDiameter, gridPos.y / _nodeDiameter);
	}

	private float GetNearestMultiplyOfNodeDiameter(float n) {
		if (n > 0) {
			return Mathf.Ceil (n / _nodeDiameter) * _nodeDiameter;
		} else if (n < 0) {
			return Mathf.Floor (n / _nodeDiameter) * _nodeDiameter;
		} else {
			return _nodeDiameter
		}
	}

	public Node GetNodeFromWorldPos(Vector2 worldPos) {
		Vector2 gridPos = WorldToGridPos (worldPos);

		Vector2 arrayGridPos = new Vector2 (gridPos.x / _nodeDiameter, gridPos.y / _nodeDiameter);

		return _grid[(int)arrayGridPos.x, (int)arrayGridPos.y];
	}

	public Vector2 FindNearestPosition(Vector2 worldPos) {
		Vector2 nearestPos = WorldToGridPos (worldPos);

		Vector2 minimumPosition = _grid [0, 0].GetGridPos ();
		Vector2 maximumPosition = _grid [_gridWidth - 1, _gridHeight - 1].GetGridPos();

		if (nearestPos.x >= minimumPosition.x && nearestPos.x <= maximumPosition.x) {
			if (nearestPos.y >= minimumPosition.y && nearestPos.y <= maximumPosition.y) {
				return nearestPos;
			}
		}
		return null;
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
