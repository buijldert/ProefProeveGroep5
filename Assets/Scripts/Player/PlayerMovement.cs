using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public static Action OnPathFinished;

    [SerializeField] private Vector2[] _path;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        for (int i = 0; i < _path.Length; i++)
        {
            while(Vector2.Distance(transform.position, _path[i]) > .01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, _path[i], 1.5f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        if(OnPathFinished != null)
            OnPathFinished();
    }
}
