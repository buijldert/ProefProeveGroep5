using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;

namespace UI.Managers
{
    public class TutorialManager : ScreenManager
    {
        public static TutorialManager instance;

        [SerializeField] private Button _playButton;
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

            screenState = MenuState.Tutorial;
        }

        protected override void PrepareScreen(MenuState state)
        {
            base.PrepareScreen(state);
        }

        protected override void StartScreen()
        {
            _playButton.onClick.AddListener(() => OnPlayButtonClicked());
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
        }

        #region UI Event

        private void OnPlayButtonClicked()
        {
            UIController.instance.GoToLevelSelectScreen();
        }

        private void OnBackButtonClicked()
        {
            UIController.instance.GoToMainMenuScreen();
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

