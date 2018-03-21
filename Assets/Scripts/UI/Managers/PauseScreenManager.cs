using UnityEngine;
using UnityEngine.UI;
using UI.Controllers;

namespace UI.Managers
{
    public class PauseScreenManager : MonoBehaviour
    {
        public static PauseScreenManager instance;

        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _toggleSound;
        [SerializeField] private Button _returnToMenu;

        private void OnEnable()
        {
            UIController.OnThemeChanged += OnThemeUpdated;
        }

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
        }

        /// <summary>
        /// Initialize the pausescreen.
        /// </summary>
        public void Init()
        {
            SubscribeToEvents();

            Time.timeScale = 0f;
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

        private void OnThemeUpdated(UITheme UITheme)
        {
            _pauseScreen.GetComponent<Image>().sprite = UITheme.PauseBackground;
            _resumeButton.GetComponent<Image>().sprite = UITheme.ButtonResume;
            _toggleSound.GetComponent<Image>().sprite = UITheme.ButtonSoundToggle;
            _returnToMenu.GetComponent<Image>().sprite = UITheme.ButtonMenu;
        }

        /// <summary>
        /// Method that gets called when the resume button is clicked
        /// </summary>
        private void OnResumeButtonClicked()
        {
            Time.timeScale = 1f;
            _pauseScreen.SetActive(false);
        }

        /// <summary>
        /// Method that gets called when the toggle button is clicked
        /// 
        /// TODO: Hook this up to a settings/sound system
        /// </summary>
        private void OnToggleSoundButtonClicked()
        {
            //
        }

        /// <summary>
        /// Method that gets called when the return button is clicked
        /// </summary>
        private void OnReturnToMenuButtonClicked()
        {
            UnsubscribeToEvents();
            Time.timeScale = 1f;

            _pauseScreen.SetActive(false);
            UIController.instance.GoToMainMenuScreen();
        }

        #endregion

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
            UIController.OnThemeChanged -= OnThemeUpdated;
            UnsubscribeToEvents();
        }
    }
}