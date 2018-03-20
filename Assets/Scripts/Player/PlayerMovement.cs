using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public static Action OnPathFinished;

    [SerializeField] private Pathfinding _pathfinding;

    [SerializeField] private Vector2[] _path;

    private int _pathIndex;

    private void Start()
    {
        //StartCoroutine(Move());
    }

    private void Update()
    {
        if(_pathfinding.GetPath().Count > 0) { 
            if (_pathIndex < _pathfinding.GetPath().Count) {
                transform.position = Vector2.MoveTowards(transform.position, _pathfinding.GetPath()[_pathIndex].GetWorldPos(), 1.5f * Time.deltaTime);
                if ((Vector2)transform.position == _pathfinding.GetPath()[_pathIndex].GetWorldPos()) {
                    _pathIndex++;
                }
            }
        }
    }

    /// <summary>
    /// Moves the player along a set path for demo purposes.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Move()
    {
        //yield return new WaitForSeconds(20f);
        for (int i = 0; i < _path.Length; i++)
        {
            while(Vector2.Distance(transform.position, _path[i]) > .01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, _path[i], 1.5f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        if(OnPathFinished != null)
        {
            OnPathFinished();
        }   
    }
}
