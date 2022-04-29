using UnityEngine;
using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class HeadbobbingSettingViewModel : ViewModel
    {   
        private bool _headbobbingSetting = true;
        [Binding]
        public bool HeadbobbingSetting
        {
            get { return this._headbobbingSetting; }
            set
            {
                this._headbobbingSetting = value;
                this.OnPropertyChanged("HeadbobbingSetting");
            }
        }
    }
}
