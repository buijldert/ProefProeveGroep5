using UnityEngine.UI;
using UnityEngine;
using UI.Base;
using UI.Controllers;

namespace UI.Managers
{
    public class LevelSelectManager : ScreenManager
    {
        public static LevelSelectManager instance;

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

            screenState = MenuState.LevelSelect;
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
            
        }

        #region UI Event



        #endregion

        protected override void StopScreen()
        {
            
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}