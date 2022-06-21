using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class SoundSettingViewModel : ViewModel
    {
        [Binding]
        public float VolumeSetting
        {
            get { return GlobalOptions.Main.Volume; }
            set
            {
                GlobalOptions.Main.Volume = value;
                this.OnPropertyChanged("VolumeSetting");
            }
        }
    }
}
