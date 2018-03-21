using System;
using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;
using UnityEngine.UI;

namespace Environment {
    public class Lava : MonoBehaviour {
        public static Action OnLavaEngulfs;

        [SerializeField]private float _lavaSpeed = 0.75f;

        [SerializeField]private Camera _mainCamera;
        [SerializeField]private Transform _player;

        private void OnEnable()
        {
            TilePlacer.OnLavaStart += StartMoving;
        }

        private void OnDisable()
        {
            TilePlacer.OnLavaStart -= StartMoving;
        }

        private void StartMoving()
        {
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            Vector3 stageDimensions = new Vector3(Screen.width, Screen.height, 0);

            while (transform.position.y < stageDimensions.y)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, stageDimensions.y), _lavaSpeed * Time.deltaTime);
                //print(_mainCamera.ScreenToWorldPoint(transform.position).y);
                if (transform.position.y > _player.position.y)
                {
                    
                    print("dead");
                    if (OnLavaEngulfs != null)
                        OnLavaEngulfs();

                    StopAllCoroutines();
                }
                yield return new WaitForEndOfFrame();
            }
        }   
    }
}