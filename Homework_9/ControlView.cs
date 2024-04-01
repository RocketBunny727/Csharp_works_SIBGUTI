using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

namespace Homework_9
{
    public partial class ControlView : UserControl
    {
        private double _currentValue;
        private string _data;

        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<ControlView, bool>(nameof(IsExpanded));

        public bool IsExpanded
        {
            get => GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public static readonly StyledProperty<double> MinimumProperty =
            AvaloniaProperty.Register<ControlView, double>(nameof(Minimum));

        public double Minimum
        {
            get => GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly StyledProperty<double> MaximumProperty =
            AvaloniaProperty.Register<ControlView, double>(nameof(Maximum));

        public double Maximum
        {
            get => GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
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
