﻿using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;

namespace UI.Managers
{
    public class CreditsManager : ScreenManager
    {
        public static CreditsManager instance;

        [SerializeField] private Button _backButton;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        /// <summary>
        /// Singleton code, and it sets the screenState of this script
        /// </summary>
        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.Credits;
        }

        /// <summary>
        /// Calls the base class to check the current MenuState
        /// </summary>
        /// <param name="state">The current state of the UI.</param>
        protected override void PrepareScreen(MenuState state)
        {
            base.PrepareScreen(state);
        }

        /// <summary>
        /// Will be called when we are on this particular screen
        /// </summary>
        protected override void StartScreen()
        {
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
        }

        #region UI Event

        /// <summary>
        /// Method that gets called when the back button is clicked
        /// </summary>
        private void OnBackButtonClicked()
        {
            UIController.instance.GoToMainMenuScreen();
        }

        #endregion

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}