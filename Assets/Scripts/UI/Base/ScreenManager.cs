using UI.Controllers;
using UnityEngine;

namespace UI.Base
{
    public abstract class ScreenManager : MonoBehaviour
    {
        protected MenuState screenState;

        protected virtual void OnEnable()
        {
            UIController.OnScreenChanged += PrepareScreen;
        }

        /// <summary>
        /// Use this for the Singleton Implementation
        /// </summary>
        protected abstract void Awake();

        protected virtual void PrepareScreen(MenuState state)
        {
            if (state == screenState)
            {
                StartScreen();
            }
            else
            {
                StopScreen();
            }
        }

        protected abstract void StartScreen();

        protected abstract void StopScreen();

        protected virtual void OnDisable()
        {
            UIController.OnScreenChanged -= PrepareScreen;
        }
    }
}