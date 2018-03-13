using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
	[SerializeField]private int _gridWidth;
	[SerializeField]private int _gridHeight;

	private float _nodeDiameter = 1;

	private Node[,] _grid;
	private float _nodeRadius;

	private void Start() {
		_grid = new Node[_gridWidth, _gridHeight];

		_nodeRadius = _nodeDiameter / 2f;

		CreateGrid ();

		Debug.Log (FindNearestPosition (new Vector2 (-3.0f, -0.9f)));
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

		worldPos.x = (gridPos.x - (_gridWidth / 2f) + _nodeRadius) + transform.position.x;
		worldPos.y = (gridPos.y - (_gridHeight / 2f) + _nodeRadius) + transform.position.y;

		return worldPos;
	}

	public Vector2 WorldToGridPos(Vector2 worldPos) {
		Vector2 gridPos = new Vector2 ();


		gridPos.x = (worldPos.x + (_gridWidth / 2f) - _nodeRadius) - transform.position.x;
		gridPos.y = (worldPos.y + (_gridHeight / 2f) - _nodeRadius) - transform.position.y;
		
		return new Vector2(gridPos.x / _nodeDiameter, gridPos.y / _nodeDiameter);
	}


	public Node GetNodeFromWorldPos(Vector2 worldPos) {
		Vector2 gridPos = WorldToGridPos (worldPos);

		Vector2 arrayGridPos = new Vector2 (gridPos.x / _nodeDiameter, gridPos.y / _nodeDiameter);

		return _grid[(int)arrayGridPos.x, (int)arrayGridPos.y];
	}

	public Vector2 FindNearestPosition(Vector2 worldPos) {
		Vector2 nearestPos = new Vector2();

		nearestPos = new Vector2 (Mathf.Floor(worldPos.x), Mathf.Floor(worldPos.y));

		Vector2 minimumPosition = _grid [0, 0].GetWorldPos ();
		Vector2 maximumPosition = _grid [_gridWidth - 1, _gridHeight - 1].GetWorldPos();

		if (nearestPos.x >= minimumPosition.x - _nodeRadius && nearestPos.x <= maximumPosition.x + _nodeRadius) {
			if (nearestPos.y >= minimumPosition.y - _nodeRadius && nearestPos.y <= maximumPosition.y + _nodeRadius) {
				return nearestPos;
			}
		}
		return Vector2.zero;
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
