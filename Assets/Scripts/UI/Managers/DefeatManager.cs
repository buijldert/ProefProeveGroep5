﻿using UI.Base;
using UI.Controllers;
using UI.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Managers
{
    public class DefeatManager : ScreenManager
    {
        public static DefeatManager instance;

        [SerializeField] private Image _screenBackground;
        [SerializeField] private Button _backButton;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.Defeat;
        }

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

        protected override void OnThemeUpdated(UITheme UITheme)
        {
            _screenBackground.sprite = UITheme.DefeatBackground;
            _backButton.GetComponent<Image>().sprite = UITheme.ButtonBack;
        }

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