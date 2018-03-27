using System;
using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;
using UnityEngine.UI;
using UI.Controllers;

namespace Environment {
    public class Lava : MonoBehaviour {
        public static Action OnLavaEngulfs;

        [SerializeField]private float _lavaSpeed = 0.75f;

        [SerializeField]private Camera _mainCamera;
        [SerializeField]private Transform _player;

        private Vector2 _startPos;

        private void OnEnable()
        {
            TilePlacer.OnLavaStart += StartMoving;
            OnLavaEngulfs += ResetLavaPosition;
            PlayerMovement.OnPlayerVictory += ResetLavaPosition;
        }

        private void Start()
        {
            _startPos = transform.position;
        }

        /// <summary>
        /// Starts the lava moving up.
        /// </summary>
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
                if (transform.position.y > _player.position.y)
                {
                    UIController.instance.GoToDefeatScreen();

                    if (OnLavaEngulfs != null)
                        OnLavaEngulfs();

                    StopAllCoroutines();
                }
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Resets the lava position for the game restart.
        /// </summary>
        private void ResetLavaPosition()
        {
            transform.position = _startPos;
        }

        private void OnDisable()
        {
            TilePlacer.OnLavaStart -= StartMoving;
            OnLavaEngulfs -= ResetLavaPosition;
            PlayerMovement.OnPlayerVictory -= ResetLavaPosition;
        }
    }
}