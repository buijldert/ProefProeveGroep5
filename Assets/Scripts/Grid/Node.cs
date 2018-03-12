using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	private Vector2 _gridPos;
	private Vector2 _worldPos;

	public Node(Vector2 gridPos, Vector2 worldPos) {
		_gridPos = gridPos;
		_worldPos = worldPos;
	}

	public Vector2 GetGridPos() {
		return _gridPos;
	}

	public Vector2 GetWorldPos () {
		return _worldPos;
	}
}
