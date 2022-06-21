using UnityWeld.Binding;

namespace ProjectWendigo
{
    [Binding]
    public class InvertYSettingViewModel : ViewModel
    {
        [Binding]
        public bool InvertYSetting
        {
            get { return GlobalOptions.Main.InvertY; }
            set
            {
                GlobalOptions.Main.InvertY = value;
                this.OnPropertyChanged("InvertYSetting");
            }
        }
    }
}
