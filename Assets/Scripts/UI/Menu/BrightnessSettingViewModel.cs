using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class BrightnessSettingViewModel : ViewModel
    {
        [Binding]
        public float BrightnessSetting
        {
            get { return GlobalOptions.Main.Brightness; }
            set
            {
                GlobalOptions.Main.Brightness = value;
                this.OnPropertyChanged("BrightnessSetting");
            }
        }
    }
}
