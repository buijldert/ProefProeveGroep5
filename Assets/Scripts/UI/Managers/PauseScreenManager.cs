using UnityEngine;
using UnityEngine.UI;
using System;
using UI.Controllers;

namespace UI.Managers
{
    public class PauseScreenManager : MonoBehaviour
    {
        public static PauseScreenManager instance;

        public static Action<bool> IsGamePaused;

        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _toggleSound;
        [SerializeField] private Button _returnToMenu;

        private bool _isSoundActive; // Demo, remove this in final build

        /// <summary>
        /// Singleton code.
        /// </summary>
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }

        private void Start()
        {
            _pauseScreen.SetActive(false);
            _toggleSound.GetComponent<Image>().color = Color.green;
        }

        /// <summary>
        /// Initialize the pausescreen.
        /// </summary>
        public void Init()
        {
            SubscribeToEvents();
            IsGamePausedActionCall(true);
            _pauseScreen.SetActive(true);
        }

        /// <summary>
        /// Subscribes to events.
        /// </summary>
        private void SubscribeToEvents()
        {
            _resumeButton.onClick.AddListener(() => OnResumeButtonClicked());
            _toggleSound.onClick.AddListener(() => OnToggleSoundButtonClicked());
            _returnToMenu.onClick.AddListener(() => OnReturnToMenuButtonClicked());
        }

        #region UI Events

        /// <summary>
        /// Method that gets called when the resume button is clicked
        /// </summary>
        private void OnResumeButtonClicked()
        {
            IsGamePausedActionCall(false);
            _pauseScreen.SetActive(false);
        }

        /// <summary>
        /// Method that gets called when the toggle button is clicked
        /// 
        /// TODO: Hook this up to a settings/sound system
        /// </summary>
        private void OnToggleSoundButtonClicked()
        {
            _isSoundActive = !_isSoundActive;
            Color color = (_isSoundActive) ? Color.green : Color.red;
            _toggleSound.GetComponent<Image>().color = color;
        }

        /// <summary>
        /// Method that gets called when the return button is clicked
        /// </summary>
        private void OnReturnToMenuButtonClicked()
        {
            UnsubscribeToEvents();

            _pauseScreen.SetActive(false);
            UIController.instance.GoToMainMenuScreen();
        }

        #endregion

        /// <summary>
        /// Function to clean up the code just a tiny bit. Will fire the IsGamePaused action.
        /// </summary>
        /// <param name="isGamePaused">If set to <c>true</c> is game paused.</param>
        private void IsGamePausedActionCall(bool isGamePaused)
        {
            if (IsGamePaused != null)
            {
                IsGamePaused(isGamePaused);
            }
        }

        /// <summary>
        /// Unsubscribes to events.
        /// </summary>
        private void UnsubscribeToEvents()
        {
            _resumeButton.onClick.RemoveAllListeners();
            _toggleSound.onClick.RemoveAllListeners();
            _returnToMenu.onClick.RemoveAllListeners();
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }
    }
}