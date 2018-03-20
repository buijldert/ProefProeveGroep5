using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	private Vector2 _gridPos;
	private Vector2 _worldPos;
	private bool _walkable = true;
	private TileType _tileType = TileType.None;

	public Node(Vector2 gridPos, Vector2 worldPos, bool walkable) {
		_gridPos = gridPos;
		_worldPos = worldPos;
		_walkable = walkable;
	}

	public Vector2 GetGridPos() {
		return _gridPos;
	}

	public Vector2 GetWorldPos () {
		return _worldPos;
	}

	public bool GetWalkable() {

		return _walkable;
	}

	public void SetWalkable(bool value) {
		_walkable = value;
	}

	public TileType GetTileType (){
		return _tileType;
	}

	public void SetTileType (TileType value) {
		_tileType = value;
	}
}
