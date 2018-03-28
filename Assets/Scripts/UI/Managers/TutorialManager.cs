using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;
using UI.ScriptableObjects;

namespace UI.Managers
{
    public class TutorialManager : ScreenManager
    {
        public static TutorialManager instance;

        [SerializeField] private Image _screenBackground;
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

        /// <summary>
        /// Will be called when we are on this particular screen
        /// </summary>
        protected override void StartScreen()
        {
            _playButton.onClick.AddListener(() => OnPlayButtonClicked());
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
        }

        #region UI Event

        protected override void OnThemeUpdated(UITheme UITheme)
        {
            _screenBackground.sprite = UITheme.BackdropC;
            _playButton.GetComponent<Image>().sprite = UITheme.ButtonPlay;
            _backButton.GetComponent<Image>().sprite = UITheme.ButtonBack;
        }

        /// <summary>
        /// Method that gets called when the play button is clicked
        /// </summary>
        private void OnPlayButtonClicked()
        {
            UIController.instance.GoToLevelSelectScreen();
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
            _playButton.onClick.RemoveAllListeners();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}

