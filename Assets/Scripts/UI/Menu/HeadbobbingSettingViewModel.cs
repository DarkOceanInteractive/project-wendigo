using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class HeadbobbingSettingViewModel : ViewModel
    {
        [Binding]
        public bool HeadbobbingSetting
        {
            get { return GlobalOptions.Main.Headbobbing; }
            set
            {
                GlobalOptions.Main.Headbobbing = value;
                this.OnPropertyChanged("HeadbobbingSetting");
            }
        }
    }
}
