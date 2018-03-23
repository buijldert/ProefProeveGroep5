using System;
using UnityEngine;
using UI.Controllers;
using Environment;

public class PlayerMovement : MonoBehaviour {
    public static Action OnPlayerVictory;

    [SerializeField] private Pathfinding _pathfinding;

    private int _pathIndex;

    private float _movementSpeed = 1.5f;
    private float _endPointY = 3.6f;

    private bool _isVictorious;
    
    private Vector2 _startPos;

    private void OnEnable()
    {
        Lava.OnLavaEngulfs += ResetPlayerPosition;
        OnPlayerVictory += ResetPlayerPosition;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        if(_pathfinding.GetPath().Count > 0) { 
            if (_pathIndex < _pathfinding.GetPath().Count) {
                transform.position = Vector2.MoveTowards(transform.position, _pathfinding.GetPath()[_pathIndex].GetWorldPos(), _movementSpeed * Time.deltaTime);
                if ((Vector2)transform.position == _pathfinding.GetPath()[_pathIndex].GetWorldPos()) {
                    _pathIndex++;
                }

                if(transform.position.y > _endPointY && !_isVictorious)
                {
                    _isVictorious = true;
                    if (OnPlayerVictory != null)
                        OnPlayerVictory();

                    UIController.instance.GoToVictoryScreen();
                }
            }
        }
    }

    private void ResetPlayerPosition()
    {
        transform.position = _startPos;
        _isVictorious = false;
        _pathIndex = 0;
    }

    private void OnDisable()
    {
        Lava.OnLavaEngulfs -= ResetPlayerPosition;
        OnPlayerVictory -= ResetPlayerPosition;
    }
}
