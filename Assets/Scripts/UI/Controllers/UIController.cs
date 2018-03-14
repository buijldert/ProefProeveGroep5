using UnityEngine;
using System.Linq;
using System;

namespace UI.Controllers
{
    public enum MenuState
    {
        MainMenu,
        Tutorial,
        Credits,
        LevelSelect,
        GameView,
        Victory,
        Defeat,
    }

    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public static Action<MenuState> OnScreenChanged;

        [SerializeField] private GameObject[] holders;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }

        private void SetState(MenuState state)
        {
            TurnHoldersInactive();
            holders[(int)state].SetActive(true);

            if (OnScreenChanged != null)
            {
                OnScreenChanged(state);
            }
        }

        private void TurnHoldersInactive()
        {
            foreach (GameObject holder in holders.Where(holder => holder.activeSelf))
            {
                holder.SetActive(false);
            }
        }

        #region GoTo methods
        public void GoToMainMenuScreen()
        {
            SetState(MenuState.MainMenu);
        }

        public void GoToTutorialScreen()
        {
            SetState(MenuState.Tutorial);
        }

        public void GoToCreditsScreen()
        {
            SetState(MenuState.Credits);
        }

        public void GoToLevelSelectScreen()
        {
            SetState(MenuState.LevelSelect);
        }

        public void GoToGameViewScreen()
        {
            SetState(MenuState.GameView);
        }

        /// <summary>
        /// Replace these function with classes later on, these are supposed to be pop-ups
        /// </summary>
        public void GoToVictoryScreen()
        {
            SetState(MenuState.Victory);
        }

        public void GoToDefeatScreen()
        {
            SetState(MenuState.Defeat);
        }
        #endregion
    }
}

