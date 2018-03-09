using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiles {

    public class TilePlacer : MonoBehaviour {

        public static Action<GameObject> OnChangeTile;

        private bool _canSpawnTile;

        public void PickTile(GameObject tileToPick)
        {
            if (_canSpawnTile)
            {
                _canSpawnTile = false;
                GameObject newTile = Instantiate(tileToPick);
                if (OnChangeTile != null)
                    OnChangeTile(newTile);
            }

        }

        public void ClickedButtonCheck()
        {
            _canSpawnTile = true;
        }
    }
}