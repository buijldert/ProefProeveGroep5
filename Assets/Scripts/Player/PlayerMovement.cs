﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Pathfinding _pathfinding;

    private int _pathIndex;

    private float _movementSpeed = 1.5f;

    private void Update()
    {
        if(_pathfinding.GetPath().Count > 0) { 
            if (_pathIndex < _pathfinding.GetPath().Count) {
                transform.position = Vector2.MoveTowards(transform.position, _pathfinding.GetPath()[_pathIndex].GetWorldPos(), _movementSpeed * Time.deltaTime);
                if ((Vector2)transform.position == _pathfinding.GetPath()[_pathIndex].GetWorldPos()) {
                    _pathIndex++;
                }
            }
        }
    }
}
