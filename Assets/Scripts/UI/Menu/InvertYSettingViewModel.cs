using UnityEngine;
using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class InvertYSettingViewModel : ViewModel
    {   
        private bool _invertYSetting = false;
        [Binding]
        public bool InvertYSetting
        {
            get { return this._invertYSetting; }
            set
            {
                this._invertYSetting = value;
                this.OnPropertyChanged("InvertYSetting");
            }
        }
    }
}
