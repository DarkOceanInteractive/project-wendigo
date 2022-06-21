using UnityEngine;

namespace ProjectWendigo
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _optionsMenu;
        [SerializeField] private GameObject _optionsMenuBackButton;
        [SerializeField] private GameObject _PlayerCamera;

        private bool _isOpen = false;

        private void Update()
        {
            if (Singletons.Main.Input.PlayerPaused)
            {
                if (this._isOpen)
                {
                    this.ClosePauseMenu();
                }
                else
                {
                    this.OpenPauseMenu();
                }
            }
        }

        public void OpenPauseMenu()
        {
            Singletons.Main.Input.ShowCursor();
            this.SetPauseMenuVisibility(true);
            Time.timeScale = 0;
            Singletons.Main.Camera.LockCamera();
        }

        public void ClosePauseMenu()
        {
            Singletons.Main.Input.HideCursor();
            this.SetPauseMenuVisibility(false);
            Time.timeScale = 1;
            Singletons.Main.Camera.UnlockCamera();
            GlobalOptions.Main.Save();
        }

        private void SetPauseMenuVisibility(bool visible)
        {
            this._menu.SetActive(visible);
            this._mainMenu.SetActive(!visible);
            this._optionsMenu.SetActive(visible);
            this._optionsMenuBackButton.SetActive(!visible);
            this._isOpen = visible;
        }
    }
}
