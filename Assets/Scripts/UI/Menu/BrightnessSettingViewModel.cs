using UnityEngine;
using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class BrightnessSettingViewModel : ViewModel
    {   
        private float _brightnessSetting = 0;
        [Binding]
        public float BrightnessSetting
        {
            get { return this._brightnessSetting; }
            set
            {
                this._brightnessSetting = value;
                this.OnPropertyChanged("BrightnessSetting");
            }
        }
    }
}
