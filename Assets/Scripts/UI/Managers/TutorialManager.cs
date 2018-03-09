using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;

namespace UI.Managers
{
    public class TutorialManager : ScreenManager
    {
        public static TutorialManager instance;

        [SerializeField] private Button _backButton;
        [SerializeField] private Button _playButton;

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

            screenState = MenuState.Tutorial;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void PrepareScreen(MenuState state)
        {
            base.PrepareScreen(state);
        }

        protected override void StartScreen()
        {
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
            _playButton.onClick.AddListener(() => OnPlayButtonClicked());
        }

        #region UI Event

        private void OnBackButtonClicked()
        {
            _uiController.GoToMainMenuScreen();
        }

        private void OnPlayButtonClicked()
        {
            _uiController.GoToLevelSelectScreen();
        }

        #endregion

        protected override void StopScreen()
        {
            _backButton.onClick.RemoveAllListeners();
            _playButton.onClick.RemoveAllListeners();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}

