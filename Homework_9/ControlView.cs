using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

namespace Homework_9
{
    public partial class ControlView : UserControl
    {
        private const double _maxValue = 10005555;
        private const double _minValue = 0;
        private double _currentValue;
        private string _data;

        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<ControlView, bool>(nameof(IsExpanded));

        public bool IsExpanded
        {
            get => GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public double MaxValue
        {
            get { return _maxValue; }
        }

        public double MinValue
        {
            get { return _minValue; }
        }

        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
            }
        }

        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;
            }
        }

        public ControlView()
        {
            InitializeComponent();
            var controlButton = this.FindControl<Button>("ControlButton");
            controlButton.Click += ControlButton_Click;
            DataContext = this;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
            IsExpanded = !IsExpanded;
            var slider = this.FindControl<Slider>("Slider");
            slider.IsVisible = IsExpanded;
            slider.Minimum = MinValue; slider.Maximum = MaxValue;

            var window = this.VisualRoot as Window;
            if (window != null)
            {
                if (!IsExpanded)
                {
                    Update(180, 180);
                }
                else
                {
                    Update(360, 180);
                }
            }
        }

        public void Update(double width, double height)
        {
            var controlButton = this.FindControl<Button>("ControlButton");
            controlButton.Width = width;
            controlButton.Height = height;
            var dataTextBlock = this.FindControl<TextBlock>("DataTextBlock");
        }
    }
}
