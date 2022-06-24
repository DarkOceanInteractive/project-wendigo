using UnityEngine;

namespace ProjectWendigo
{
    public class PlayBackgroundMusic : MonoBehaviour
    {
        [SerializeField] private string _backgroundMusicName;

        private void Awake()
        {
            Singletons.Main.Sound.Play(this._backgroundMusicName);
        }
    }
}
