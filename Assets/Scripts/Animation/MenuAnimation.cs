using DG.Tweening;
using UnityEngine;

namespace Animation
{
    public class MenuAnimation : MonoBehaviour
    {

        [SerializeField] private Transform[] _animatedObjects;

        private float _animDuration = .25f;

        private void OnEnable()
        {
            AnimateMenuButtons();
        }

        private void AnimateMenuButtons()
        {
            for (int i = 0; i < _animatedObjects.Length; i++)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(_animatedObjects[i].DOScale(0, 0f));
                sequence.AppendInterval(_animDuration);
                sequence.AppendInterval(i * _animDuration);
                sequence.Append(_animatedObjects[i].DOScale(1.25f, _animDuration));
                sequence.Append(_animatedObjects[i].DOScale(1, _animDuration));
            }
        }
    }
}