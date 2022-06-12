using UnityEngine;

namespace ProjectWendigo
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private void Awake()
        {
            Singletons.Main.Input.ShowCursor();
        }

        public void PlayGame()
        {
            this._player.SetActive(true);
            Singletons.Main.Option.gameObject.GetComponent<PauseMenu>().enabled = true;
            this.gameObject.transform.parent.gameObject.SetActive(false);
            Singletons.Main.Scene.GoToNextScene();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}