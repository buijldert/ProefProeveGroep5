using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
	[SerializeField] private Grid _grid;
	private List<Node> _path;

	private Dictionary<TileType, Dictionary<Vector2, List<TileType>>> _avalibleTilesForNode = new Dictionary<TileType, Dictionary<Vector2, List<TileType>>> ();

	public List<Node> GetPath() {
		return _path;
	}

	private void Start() {

		_path = new List<Node> ();

		List<TileType> topNeighbour = new List<TileType> (){TileType.Vertical, TileType.BottomLeft, TileType.BottomRight };
		List<TileType> leftNeighbour = new List<TileType> (){ TileType.BottomRight, TileType.Horizontal, TileType.TopRight };
		List<TileType> rightNeighbour = new List<TileType> (){ TileType.BottomLeft, TileType.Horizontal, TileType.TopLeft };
		List<TileType> bottomNeighbour = new List<TileType> (){ TileType.Vertical, TileType.TopLeft, TileType.TopLeft };

		//Bottom left tile
		Dictionary<Vector2, List<TileType>> bottomLeftDic = new Dictionary<Vector2, List<TileType>> ();

		bottomLeftDic.Add (new Vector2 (0, -1), bottomNeighbour);
		bottomLeftDic.Add (new Vector2 (-1, 0), leftNeighbour);

		_avalibleTilesForNode.Add (TileType.BottomLeft, bottomLeftDic);


		//Bottom right tile
		Dictionary<Vector2, List<TileType>> bottomRightDic = new Dictionary<Vector2, List<TileType>> ();

		bottomRightDic.Add (new Vector2 (0, -1), bottomNeighbour);
		bottomRightDic.Add (new Vector2 (1, 0), rightNeighbour);

		_avalibleTilesForNode.Add (TileType.BottomRight, bottomRightDic);

		//Top left tile
		Dictionary<Vector2, List<TileType>> topLeftDic = new Dictionary<Vector2, List<TileType>> ();

		topLeftDic.Add (new Vector2 (0, 1), topNeighbour);
		topLeftDic.Add (new Vector2 (-1, 0), leftNeighbour);

		_avalibleTilesForNode.Add (TileType.TopLeft, topLeftDic);

		//Top right tile
		Dictionary<Vector2, List<TileType>> topRightDic = new Dictionary<Vector2, List<TileType>> ();

		topRightDic.Add (new Vector2 (0, 1), topNeighbour);
		topRightDic.Add (new Vector2 (1, 0), rightNeighbour);

		_avalibleTilesForNode.Add (TileType.TopRight, topRightDic);

		//Horizontal tile
		Dictionary<Vector2, List<TileType>> horizontalDic = new Dictionary<Vector2, List<TileType>> ();

		horizontalDic.Add (new Vector2 (-1, 0), leftNeighbour);
		horizontalDic.Add (new Vector2 (1, 0), rightNeighbour);

		_avalibleTilesForNode.Add (TileType.Horizontal, horizontalDic);

		//Vertical tile
		Dictionary<Vector2, List<TileType>> verticalDic = new Dictionary<Vector2, List<TileType>> ();

		verticalDic.Add (new Vector2 (0, 1), topNeighbour);
		verticalDic.Add (new Vector2 (0, -1), bottomNeighbour);

		_avalibleTilesForNode.Add (TileType.Vertical, verticalDic);

	}


	public void CalculatePath(Node nextNode, System.Action callback) {
		if (_path.Count > 0) {
			Node currentNode = _path [_path.Count - 1];

			Vector2 neighbour = nextNode.GetGridPos () - currentNode.GetGridPos ();
			TileType nextType = nextNode.GetTileType ();



			TileType currentType = currentNode.GetTileType ();

			if (_avalibleTilesForNode [currentType].ContainsKey (neighbour)) {
				if (_avalibleTilesForNode [currentType] [neighbour].Contains (nextType) && nextNode.GetWalkable ()) {
					_path.Add (nextNode);
					callback ();

					return;
				} 
			} 
			nextNode.SetTileType (TileType.None);
		} else {
			_path.Add (nextNode);

			callback();
		}
	}

}
