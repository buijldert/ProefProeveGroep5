using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour {

    [SerializeField] private Transform[] _animatedObjects;

    private void OnEnable()
    {
        for (int i = 0; i < _animatedObjects.Length; i++)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_animatedObjects[i].DOScaleY(0, 0f));
            sequence.AppendInterval(0.2f);
            sequence.AppendInterval(i * .5f);
            sequence.Append(_animatedObjects[i].DOScaleY(1, .5f));
        }
        

    }
}
