using UnityEngine;
using UnityEngine.UI;
using System;
using UI.Controllers;

public class PauseScreen : MonoBehaviour
{
    public static PauseScreen instance;

    public static Action<bool> IsGamePaused;

    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _toggleSound;
    [SerializeField] private Button _returnToMenu;

    private bool _isSoundActive; // Demo, remove this in final build

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

    public void Init()
    {
        SubscribeToEvents();
        IsGamePausedActionCall(true);
        _pauseScreen.SetActive(true);
    }

    private void SubscribeToEvents()
    {
        _resumeButton.onClick.AddListener(() => OnResumeButtonClicked());
        _toggleSound.onClick.AddListener(() => OnToggleSoundButtonClicked());
        _returnToMenu.onClick.AddListener(() => OnReturnToMenuButtonClicked());
    }

    #region UI Events

    private void OnResumeButtonClicked()
    {
        IsGamePausedActionCall(false);
        _pauseScreen.SetActive(false);
    }

    private void OnToggleSoundButtonClicked()
    {
        _isSoundActive = !_isSoundActive;
        Color color = (_isSoundActive) ? Color.green : Color.red;
        _toggleSound.GetComponent<Image>().color = color;

        //TODO: Hook this up to a settings/sound system
    }

    private void OnReturnToMenuButtonClicked()
    {
        UnsubscribeToEvents();

        _pauseScreen.SetActive(false);
        UIController.instance.GoToMainMenuScreen();
    }

    #endregion

    private void IsGamePausedActionCall(bool isGamePaused)
    {
        if (IsGamePaused != null)
        {
            IsGamePaused(isGamePaused);
        }
    }

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
