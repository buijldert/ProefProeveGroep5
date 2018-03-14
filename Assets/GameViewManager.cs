using UI.Base;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Managers
{
    public class GameViewManager : ScreenManager
    {
        public static GameViewManager instance;

        [SerializeField] private Button _pauseButton;

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
            //_pauseButton.onClick.AddListener(() => OnPauseButtonClicked());

            PlayerMovement.OnPathFinished += OnPathFinished;
        }

        private void OnPathFinished()
        {
            UIController.instance.GoToVictoryScreen();
        }

        #region UI Event

        /// <summary>
        /// Method that gets called when the back button is clicked
        /// </summary>
        private void OnPauseButtonClicked()
        {
            PauseScreenManager.instance.Init();
        }

        #endregion

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            //_pauseButton.onClick.RemoveAllListeners();

            PlayerMovement.OnPathFinished -= OnPathFinished;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
