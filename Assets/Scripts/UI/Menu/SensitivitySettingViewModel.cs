using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class SensitivitySettingViewModel : ViewModel
    {
        [Binding]
        public float SensitivitySetting
        {
            get { return GlobalOptions.Main.Sensitivity; }
            set
            {
                GlobalOptions.Main.Sensitivity = value;
                this.OnPropertyChanged("SensitivitySetting");
            }
        }
    }
}
