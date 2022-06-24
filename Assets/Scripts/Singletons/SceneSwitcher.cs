using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectWendigo
{
    public class SceneSwitcher : MonoBehaviour
    {
        public UnityEvent OnLoadScene;

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _playerBody;

        public void GoToNextScene()
        {
            int nextSceneId = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(this.LoadScene(nextSceneId, this.OnAfterSwitch));
        }

        private IEnumerator LoadScene(int sceneBuildIndex, Action callback)
        {
            Singletons.Main.Save.Save();
            yield return Singletons.Main.Fade.WaitForFadeOutEffect();
            var asyncLoadScene = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
            yield return new WaitUntil(() => asyncLoadScene.isDone);
            callback();
            yield return Singletons.Main.Fade.WaitForFadeInEffect();
        }

        private void OnAfterSwitch()
        {
            GameObject playerSpawn = GameObject.Find("PlayerSpawn");
            if (playerSpawn == null)
            {
                Debug.LogWarning("No player spawn found");
                return;
            }
            this._player.transform.position = playerSpawn.transform.position;
            this._player.transform.rotation = playerSpawn.transform.rotation;
            this._playerBody.transform.localPosition = Vector3.zero;
            this._playerBody.transform.localRotation = Quaternion.identity;
            this.OnLoadScene?.Invoke();
        }
    }
}