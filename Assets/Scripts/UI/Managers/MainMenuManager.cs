using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;

namespace UI.Managers
{
    public class MainMenuManager : ScreenManager
    {
        public static MainMenuManager instance;

        [SerializeField] private Image _screenBackground;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _tutorialButton;
        [SerializeField] private Button _creditsButton;

        protected override void OnEnable()
        {
            base.OnEnable();

            _playButton.onClick.AddListener(() => OnPlayButtonClicked());
            _tutorialButton.onClick.AddListener(() => OnTutorialButtonClicked());
            _creditsButton.onClick.AddListener(() => OnCreditsButtonClicked());
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

            screenState = MenuState.MainMenu;
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
            _playButton.onClick.AddListener(() => OnPlayButtonClicked());
            _tutorialButton.onClick.AddListener(() => OnTutorialButtonClicked());
            _creditsButton.onClick.AddListener(() => OnCreditsButtonClicked());
        }

        #region UI Events

        protected override void OnThemeUpdated(UITheme UITheme)
        {
            _screenBackground.sprite = UITheme.BackdropA;
            _playButton.GetComponent<Image>().sprite = UITheme.ButtonPlay;
            _tutorialButton.GetComponent<Image>().sprite = UITheme.ButtonTutorial;
            _creditsButton.GetComponent<Image>().sprite = UITheme.ButtonCredits;
        }

        /// <summary>
        /// Method that gets called when the play button is clicked
        /// </summary>
        private void OnPlayButtonClicked()
        {
            UIController.instance.GoToLevelSelectScreen();
        }

        /// <summary>
        /// Method that gets called when the tutorial button is clicked
        /// </summary>
        private void OnTutorialButtonClicked()
        {
            UIController.instance.GoToTutorialScreen();
        }

        /// <summary>
        /// Method that gets called when the credits button is clicked
        /// </summary>
        private void OnCreditsButtonClicked()
        {
            UIController.instance.GoToCreditsScreen();
        }

        #endregion

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            _playButton.onClick.RemoveAllListeners();
            _tutorialButton.onClick.RemoveAllListeners();
            _creditsButton.onClick.RemoveAllListeners();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}

