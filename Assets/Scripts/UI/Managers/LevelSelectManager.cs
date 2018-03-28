using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;
using System;
using UI.ScriptableObjects;

namespace UI.Managers
{
    public class LevelSelectManager : ScreenManager
    {
        public static LevelSelectManager instance;

        public static Action OnGameStarted;

        [SerializeField] private Image _screenBackground;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _levelOne;

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

            screenState = MenuState.LevelSelect;
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
            _levelOne.onClick.AddListener(() => OnLevelOneButtonClicked());
        }

        #region UI Event

        protected override void OnThemeUpdated(UITheme UITheme)
        {
            _screenBackground.sprite = UITheme.BackdropB;
            _backButton.GetComponent<Image>().sprite = UITheme.ButtonBack;
            _levelOne.GetComponent<Image>().sprite = UITheme.ButtonLevelOpen;
        }

        /// <summary>
        /// Method that gets called when the back button is clicked
        /// </summary>
        private void OnBackButtonClicked()
        {
            UIController.instance.GoToMainMenuScreen();
        }

        private void OnLevelOneButtonClicked()
        {
            UIController.instance.GoToGameViewScreen();

            if (OnGameStarted != null)
                OnGameStarted();
        }

        #endregion

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            _backButton.onClick.RemoveAllListeners();
            _levelOne.onClick.RemoveAllListeners();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}