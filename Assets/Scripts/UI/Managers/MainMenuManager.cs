using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;

namespace UI.Managers
{
    public class MainMenuManager : ScreenManager
    {
        public static MainMenuManager instance;

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

        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.MainMenu;
        }

        protected override void PrepareScreen(MenuState state)
        {
            base.PrepareScreen(state);
        }

        protected override void StartScreen()
        {
            _playButton.onClick.AddListener(() => OnPlayButtonClicked());
            _tutorialButton.onClick.AddListener(() => OnTutorialButtonClicked());
            _creditsButton.onClick.AddListener(() => OnCreditsButtonClicked());
        }

        #region UI Events

        private void OnPlayButtonClicked()
        {
            UIController.instance.GoToLevelSelectScreen();
        }

        private void OnTutorialButtonClicked()
        {
            UIController.instance.GoToTutorialScreen();
        }

        private void OnCreditsButtonClicked()
        {
            UIController.instance.GoToCreditsScreen();
        }

        #endregion

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

