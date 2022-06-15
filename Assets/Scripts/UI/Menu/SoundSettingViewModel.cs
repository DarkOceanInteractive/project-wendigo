using UnityEngine;
using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class SoundSettingViewModel : ViewModel
    {
        private float _volumeSetting = 0.5f;
        [Binding]
        public float VolumeSetting
        {
            get { return this._volumeSetting; }
            set
            {
                this._volumeSetting = value;
                this.OnPropertyChanged("VolumeSetting");
            }
        }
    }
}
