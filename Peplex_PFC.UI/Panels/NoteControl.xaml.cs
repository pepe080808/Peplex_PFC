namespace Peplex_PFC.UI.Panels
{
    public partial class NoteControl
    {
        private decimal _value;
        public decimal Value { get { return _value; } set { _value = value;  UpdateUI();} }

        public NoteControl()
        {
            InitializeComponent();
        }

        public void UpdateUI()
        {
            var percent = (double)(_value * 100 / 2);
            PathStar.Width = percent <= 0 ? 0.1 : PathStar.Width * (percent / 100);
        }
    }
}
