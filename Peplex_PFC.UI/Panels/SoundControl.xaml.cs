using System.Windows;
using System.Windows.Media;

namespace Peplex_PFC.UI.Panels
{
    public partial class SoundControl
    {
        private bool _value;
        public bool Value {
            get { return _value; }
            set { _value = value; UpdateUI(); }
        }

        public SoundControl()
        {
            InitializeComponent();
        }

        private void UpdateUI()
        {
            if (!_value)
            {
                var c1 = (Color)Application.Current.Resources["DarkGrayColor"];
                var c2 = (Color)Application.Current.Resources["LightGrayColor"];
                var gs1 = new GradientStop { Color = c1, Offset = 0.0 };
                var gs2 = new GradientStop { Color = c2, Offset = 1.0 };
                var lg = new LinearGradientBrush { StartPoint = new Point(0.0, 0.5), EndPoint = new Point(1.0, 0.5), GradientStops = new GradientStopCollection { gs1, gs2 } };
                MyPath.Fill = lg; ;
            }
            else
            {
                var c1 = (Color)Application.Current.Resources["DarkGreenColor"];
                var c2 = (Color)Application.Current.Resources["LightGreenColor"];
                var gs1 = new GradientStop { Color = c1, Offset = 0.0 };
                var gs2 = new GradientStop { Color = c2, Offset = 1.0 };
                var lg = new LinearGradientBrush { StartPoint = new Point(0.0, 0.5), EndPoint = new Point(1.0, 0.5), GradientStops = new GradientStopCollection { gs1, gs2 } };
                MyPath.Fill = lg;
            }
        }
    }
}
