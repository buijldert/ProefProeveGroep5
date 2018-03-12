using UnityEngine;

namespace Tiles {

    public class DragTile : MonoBehaviour {

        public delegate Vector2 GetGridPosAction(Vector2 dropPos);
        public static GetGridPosAction OnGetGridPos;

        private GameObject _tileToDrag = null;

        private void OnEnable() {

            TilePlacer.OnChangeTile += ChangeDragTile;
        }

        private void ChangeDragTile(GameObject dragTile) {

            _tileToDrag = dragTile;
        }

        private void Update() {

            if(_tileToDrag != null)
            {
                if (Input.touchCount > 0 )
                {
                    TileDrag(Input.GetTouch(0).position);
                }
                else if (Input.GetMouseButton(0))
                {
                    TileDrag(Input.mousePosition);
                }
                else
                {
                    if (OnGetGridPos != null)
                        _tileToDrag.transform.position = OnGetGridPos(_tileToDrag.transform.position);
                    _tileToDrag = null;
                }
            }
        }

        private void TileDrag(Vector3 inputPosition) {

            Vector3 inputPos = Camera.main.ScreenToWorldPoint(inputPosition);
            _tileToDrag.transform.position = new Vector3(inputPos.x, inputPos.y, _tileToDrag.transform.position.z);
        }

        private void OnDisable() {

            TilePlacer.OnChangeTile -= ChangeDragTile;
        }
    }
}