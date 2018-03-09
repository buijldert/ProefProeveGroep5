using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;

namespace UI.Managers
{
    public class CreditsManager : ScreenManager
    {
        public static CreditsManager instance;

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

            screenState = MenuState.Credits;
        }


        protected override void PrepareScreen(MenuState state)
        {
            base.PrepareScreen(state);
        }

        protected override void StartScreen()
        {
            _backButton.onClick.AddListener(() => OnBackButtonClicked());
        }

        #region UI Event

        private void OnBackButtonClicked()
        {
            UIController.instance.GoToMainMenuScreen();
        }

        #endregion

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