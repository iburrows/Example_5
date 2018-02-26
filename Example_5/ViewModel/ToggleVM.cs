using GalaSoft.MvvmLight;

namespace Example_5.ViewModel
{
    public class ToggleVM:ViewModelBase
    {
        private int buttonValue;
        public string ButtonId { get; set; }

        public int Value {
            get { return buttonValue; }
            set { buttonValue = value; RaisePropertyChanged(); }
        }

        public ToggleVM(int value, string buttonId)
        {
            Value = value;
            ButtonId = buttonId;
        }
    }
}