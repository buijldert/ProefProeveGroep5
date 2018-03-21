using UI.Base;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Managers
{
    public class GameViewManager : ScreenManager
    {
        public static GameViewManager instance;

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _endpoint;

        [SerializeField] private Image _screenBackground;
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

            screenState = MenuState.GameView;
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

            _player.SetActive(true);
            _endpoint.SetActive(true);

            //PlayerMovement.OnPathFinished += OnPathFinished;
        }

        private void OnPathFinished()
        {
            UIController.instance.GoToVictoryScreen();
        }

        #region UI Event

        protected override void OnThemeUpdated(UITheme UITheme)
        {
            _screenBackground.sprite = UITheme.GameBackground;
            _endpoint.GetComponent<SpriteRenderer>().sprite = UITheme.Endpoint;
        }

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

            _player.SetActive(false);
            _endpoint.SetActive(false);

            //PlayerMovement.OnPathFinished -= OnPathFinished;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
